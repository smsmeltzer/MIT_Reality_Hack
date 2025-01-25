using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControl : MonoBehaviour
{
    [SerializeReference] private ShipSystem shipSystem;
    [SerializeReference] private bool useMouse = false;
    [SerializeReference] private bool isGrabbed;
    [SerializeReference] private float movementRange;
    [SerializeReference] private float mouseStartXPoint;
    [SerializeReference] private Transform grabber;
    [SerializeReference] private Transform knob;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed)
        {
           FollowGrabber(grabber);
        }
    }   

    public void FollowGrabber(Transform _grabber)
    {
        if(_grabber == null){return;}

        float newXPos = _grabber.position.x - transform.position.x;
        if(Mathf.Abs(newXPos) > movementRange)
        {newXPos = Mathf.Sign(newXPos) * movementRange;}
        knob.localPosition = Vector3.Lerp(knob.localPosition, new Vector3(newXPos,0,0),Time.deltaTime * 10);
        DisplayData();
    }

    public void OnGrab(Transform _grabber)
    {
        grabber = _grabber;
        isGrabbed = true;

    }

    public void OnRelease(Transform _grabber)
    {
        grabber = null;
        isGrabbed = false;

    }

    public void FollowMouse()
    {



        if(Mathf.Abs(mouseStartXPoint - Input.mousePosition.x) < 5){return;}
        float direction = 1;
        if(mouseStartXPoint > Input.mousePosition.x){direction = -1;}
        
        knob.localPosition = Vector3.MoveTowards(knob.localPosition ,new Vector3(movementRange * direction,0,0),Time.deltaTime );
        mouseStartXPoint = Input.mousePosition.x;
        DisplayData();
    }

    public void DisplayData()
    {
        if(shipSystem == null){return;}

        string percent = "";
        if(knob.localPosition.x == 0)
        {
            percent = "0%";
        }
        else
        {
            percent =   Mathf.RoundToInt((knob.localPosition.x/movementRange) * 100).ToString()+ "%";
        }
        shipSystem.GetDisplayText().SetText(percent);;

    }


     void OnMouseDown()
    {
        if(useMouse == false){return;}

       if(isGrabbed){return;}
       isGrabbed = true;
       mouseStartXPoint = Input.mousePosition.x;
    }

 public void OnMouseUp()
    {
         if(useMouse == false){return;}
        isGrabbed = false;
    }
}
