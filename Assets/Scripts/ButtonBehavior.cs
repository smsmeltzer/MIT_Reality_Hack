using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonBehavior : XRGrabInteractable
{
    [SerializeField] private AudioSource AudioSource;

    protected override void OnSelectEntered(SelectEnterEventArgs args) {  
        base.OnSelectEntered(args); 
        transform.position += new Vector3(0, -.01f, 0);
        AudioSource.Play();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        transform.position += new Vector3(0, .01f, 0);
    }
}
