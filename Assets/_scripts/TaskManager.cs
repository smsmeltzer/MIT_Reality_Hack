using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TaskManager : MonoBehaviour
{
    private ShipSystemManager shipSystemManager;
    //the system dictionary tracks the ship systems and the value needed to suceed
    
    [SerializeReference] private Dictionary<string, int> systemDictionary;

    //the needs attention list tracks the systems that need to be adjusted
    //ignoring systems that are not related to the current task
    [SerializeReference] private List<string> systemsThatNeedAttention;

    public string debugSystemName;
    public int debugSystemValue;
    public bool sucess = false;
     public bool debug;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(debug)
        { 
              debug = false;
            GetOutstandingSystems().Add(debugSystemName);
            GetSystemDictionary()[debugSystemName] = debugSystemValue;
            sucess = false;
        }
    }

    public void AddNewTask(string _systemName,int _value)
    {
        if (!GetOutstandingSystems().Contains(_systemName)) 
        {GetOutstandingSystems().Add(_systemName);}

        if(GetSystemDictionary().ContainsKey(_systemName))
        {
            GetSystemDictionary()[_systemName] = _value;

        }else{GetSystemDictionary().Add(_systemName,_value);}

    }


    public void CheckSystem(ShipSystem _system)
    {
        if(_system.CheckSystem(GetSystemDictionary()[_system.GetSystemName()]))
        {
            sucess = true;
            if(GetOutstandingSystems().Contains(_system.GetSystemName()))
            {GetOutstandingSystems().Remove(_system.GetSystemName());}
        }

    }


    public List<string> GetOutstandingSystems()
    {

        if(systemsThatNeedAttention == null)
        {
            systemsThatNeedAttention  = new List<string>();

        }

        return systemsThatNeedAttention;
    }



    public Dictionary<string, int> GetSystemDictionary()
    {

        if(systemDictionary == null)
        {
            systemDictionary  = new Dictionary<string, int>();

        }

        return systemDictionary;
    }


    public void PopulateSystemDictionary()
    {
        ShipSystem[] foundSystems = FindObjectsOfType<ShipSystem>();
        int count = 0;
        foreach(ShipSystem el in foundSystems)
        {
            if(systemDictionary.ContainsKey(el.GetSystemName()) == false)
            {
                count++;
                systemDictionary.Add(el.GetSystemName(),0);
                el.GetDisplayText().SetText(count.ToString());
            }

        }
        
    }

    public ShipSystemManager GetShipSystemManager()
    {
        if(shipSystemManager == null)
        {shipSystemManager = FindObjectOfType<ShipSystemManager>(); }
        return shipSystemManager;
    }

}
