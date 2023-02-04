using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int Points = 500;

    [SerializeField]
    private Vector3 openAngle;

    [SerializeField]
    private Vector3 closeAngle;

    private bool open = false;
    private bool rotating = false;

    public void Interact(PlayerController interactor)
    {
        open = !open;

        StartCoroutine(DoorRot(open));

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

    public bool CanInteract(PlayerController interactor) => !rotating;

    public bool ShouldHighlight() => false;
}
