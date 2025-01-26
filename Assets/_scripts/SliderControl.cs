using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControl : MonoBehaviour
{
    [SerializeReference] private ShipSystem shipSystem;
    [SerializeReference] private bool isGrabbed;
     [SerializeReference] private float maxDistance;
    void Start()
    {
        if(GetComponent<ConfigurableJoint>() != null)
        {
            SoftJointLimit limit = GetComponent<ConfigurableJoint>().linearLimit;
            limit.limit =  limit.limit * transform.root.localScale.x;
            GetComponent<ConfigurableJoint>().linearLimit = limit; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed)
        {
          DisplayData();
          shipSystem.SetSystemValue(GetValue());
        }
    }   



    public void OnGrab()
    {
        isGrabbed = true;

    }

    public void OnRelease()
    {
        isGrabbed = false;
        shipSystem.GetTaskManager().CheckSystem(shipSystem);
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
    if(shipSystem == null){return 0;}
        return Mathf.RoundToInt(((transform.localPosition.x) / maxDistance) * 10);
    }
}
