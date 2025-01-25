using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialControl : MonoBehaviour
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

        string percent = "";

        if(GetValue() == 360)
        {
             percent = "0";
        
        }
        else
        {
            percent =   GetValue().ToString();
        
        }
     
        shipSystem.GetDisplayText().SetText(percent);;

    }
    public int GetValue()
    {

        return Mathf.RoundToInt(transform.eulerAngles.z);
    }

}
