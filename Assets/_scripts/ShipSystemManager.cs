using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSystemManager : MonoBehaviour
{
    // the task manager tracks the system name and the value it needs to be set to
    // for on/off binary 0/1
    // this manager tracks them by name to reference the object and uses the value from taskmanager to check against
   [SerializeReference] private Dictionary<string, ShipSystem> systemObjects;

    // Start is called before the first frame update
    void Start()
    {
        GetSystemObjects();
    }

    public void Init()
    {

        
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateShipSystem(string _name,int _value)
    {
        ShipSystem systemToUpdate = GetSystemObjects()[_name];
        if(systemToUpdate == null){return;}
        systemToUpdate.SetSystemValue(_value);

     
    }



    public Dictionary<string, ShipSystem> GetSystemObjects()
    {

        if(systemObjects == null)
        {
            systemObjects  = new Dictionary<string, ShipSystem>();
            PopulateSystemDictionary();
        }

        return systemObjects;
    }

    public void PopulateSystemDictionary()
    {
        ShipSystem[] foundSystems = FindObjectsOfType<ShipSystem>();
        int count = 0;
        foreach(ShipSystem el in foundSystems)
        {
            if(systemObjects.ContainsKey(el.GetSystemName()) == false)
            {
                count++;
                systemObjects.Add(el.GetSystemName(),el);
                el.GetDisplayText().SetText(count.ToString());
            }

        }
        
    }

}
