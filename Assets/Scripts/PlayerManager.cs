using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum Role {Command, Astronaut, None};
    
    public Role myRole;

    void Start()
    {
        myRole = Role.None;
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
