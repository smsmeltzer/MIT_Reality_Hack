using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun
{
    public enum Role {Command, Astronaut, NPC, None};
    
    public Role myRole;

    void Start()
    {
        myRole = Role.None;
        if (!photonView.IsMine)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeRoleTo(Role role)
    {
        myRole = role;
    }
}
