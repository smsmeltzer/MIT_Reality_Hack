using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Unity.XR.CoreUtils;
using Photon.Realtime;

public class ChangeRoleBehavior : XRGrabInteractable
{
    public PlayerManager.Role Role;
    public Transform teleportLocation;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        GameObject player = args.interactorObject.transform.root.gameObject;
        player.GetComponent<PlayerManager>().ChangeRoleTo(Role);
        player.transform.position = teleportLocation.position;
        player.transform.rotation = teleportLocation.rotation;
        GetComponent<PhotonView>().RPC("DisableObj", RpcTarget.All);
    }

    [PunRPC]
    public void DisableObj()
    {
        gameObject.SetActive(false);
    }


}
