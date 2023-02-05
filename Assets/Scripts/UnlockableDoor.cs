using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int Cost = 500;

    [SerializeField]
    private Vector3 openAngle;

    [SerializeField]
    private Vector3 closeAngle;

    private bool open = false;
    private bool purchased = false;
    private bool rotating = false;

    [SerializeField] private AudioClip purchaseClip, openClip;

    public void Interact(PlayerController interactor)
    {
        if (!purchased && GameManager.Instance.Points >= Cost)
        {
            // Buy the door
            GameManager.Instance.Points -= Cost;
            purchased = true;
            AudioPool.Instance.PlayClip(purchaseClip, transform);
        }

        if (purchased)
        {
            open = !open;

            AudioPool.Instance.PlayClip(openClip, transform);
            StartCoroutine(DoorRot(open));
        }
    }

    IEnumerator DoorRot(bool open)
    {
        var target = open ? openAngle : closeAngle;
        var start = transform.localRotation.eulerAngles;

        rotating = true;

        for (float i = 0f; i < 1f; i+= Time.deltaTime)
        {
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(start, target, i));
            yield return null;
        }

        rotating = false;
    }

    public bool CanInteract(PlayerController interactor) => !open;

    public bool ShouldHighlight() => false;

    public string PopupText()
    {
        if (!purchased)
        {
            return "Buy door for " + Cost.ToString("C", GameManager.CurrencyFormat);
        }

        return open ? "Close door" : "Open door";
    }
}
