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
    private Collider doorCollider;
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        doorCollider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact(PlayerController interactor)
    {
        open = !open;

        transform.localRotation = Quaternion.Euler(!open ? closeAngle : openAngle);
    }

    public bool CanInteract(PlayerController interactor) => true;

    public bool ShouldHighlight() => true;
}
