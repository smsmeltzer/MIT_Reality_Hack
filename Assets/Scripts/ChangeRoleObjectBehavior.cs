using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeRoleBehavior : XRGrabInteractable
{
    public PlayerManager.Role Role;
    public Transform teleportLocation;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        GameObject player = args.interactorObject.transform.gameObject;
        player.GetComponent<PlayerManager>().ChangeRoleTo(Role);
        player.transform.position = teleportLocation.transform.position;
        player.transform.rotation = teleportLocation.transform.rotation;
    }
}
