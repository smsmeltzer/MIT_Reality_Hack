using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonBehavior : XRGrabInteractable
{
    [SerializeField] private AudioSource AudioSource;
    public float amountLowered = .01f;
    private float y;

    private void Start()
    {
        y = transform.position.y;
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args) {  
        base.OnSelectEntered(args); 
        transform.position = new Vector3(transform.position.x, y - amountLowered, transform.position.z);
        AudioSource.Play();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        transform.position = new Vector3(transform.position.x, y + amountLowered, transform.position.z);
    }
}
