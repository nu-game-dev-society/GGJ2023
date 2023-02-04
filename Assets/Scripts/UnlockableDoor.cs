using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int Points = 500;

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

        // TODO some form of opening animation or smthing
        doorCollider.enabled = !open;
        meshRenderer.enabled = !open;
    }

    public bool CanInteract(PlayerController interactor)
    {
        return !open;
    }
}
