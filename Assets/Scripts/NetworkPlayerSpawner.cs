using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;
    public Transform playerSpawn;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.Instantiate(PlayerPrefab.name, playerSpawn.position, playerSpawn.rotation);
    }
}
