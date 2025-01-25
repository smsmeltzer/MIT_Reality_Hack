using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControl : MonoBehaviour
{
   [SerializeReference] private ShipSystem shipSystem;
    [SerializeReference] private bool isGrabbed;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed)
        {
          DisplayData();
        }
    }   



    public void OnGrab()
    {
        isGrabbed = true;

    }

    public void OnRelease()
    {
        isGrabbed = false;

    }


    public void DisplayData()
    {
        if(shipSystem == null){return;}

        string percent = "off";

        if(GetValue() )
        {
             percent = "on";
        
        }

     
        shipSystem.GetDisplayText().SetText(percent);

    }
    public bool GetValue()
    {

        return transform.eulerAngles.z - 180 < 0;
    }

}
