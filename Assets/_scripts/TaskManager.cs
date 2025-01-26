using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TaskManager : MonoBehaviour
{
    private ShipSystemManager shipSystemManager;
    private TaskSelector taskSelector;
    //the system dictionary tracks the ship systems and the value needed to suceed
    
    [SerializeReference] private Dictionary<string, int> systemDictionary;

    //the needs attention list tracks the systems that need to be adjusted
    //ignoring systems that are not related to the current task
    [SerializeReference] private List<string> systemsThatNeedAttention;

    [SerializeReference] private bool gameOn;

    public string debugSystemName;
    public int debugSystemValue;
    public bool sucess = false;
     public bool debug;

     public float idleTimer; // safety check if for some reason there are no tasks in the list when there should be

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

        if(gameOn)
        {
            idleTimer += Time.deltaTime;
            if(idleTimer >= 10)
            {
                if(GetOutstandingSystems().Count == 0)
                {
                    //need to populate the list

                }
                idleTimer = 0;
            }
        }


        if(debug)
        { 
              debug = false;
         //   GetOutstandingSystems().Add(debugSystemName);
           // GetSystemDictionary()[debugSystemName] = debugSystemValue;
           CheckTaskComplete();
        }
    }


    public void SelectNewTaskGroup()
    {
        //
        int taskCount = GetTaskSelector().currentTaskCount;
        int indexOfTask = GetTaskSelector().GetNextTaskIndex(taskCount);

        //rpc call for new task
       // GetTaskSelector().GetNextTask();
        GetEventManager().TryUpdateTask(taskCount,indexOfTask);
    }

    public void SynceNewTask(int _type,int _index)
    {
        TaskObject newTask = GetTaskSelector().GetNextTask(_type,_index);
        foreach(TaskSubObject el in newTask.tasks)
        {
            //dont add a task if the system doesnt exist
            if(GetShipSystemManager().GetSystemObjects().ContainsKey(el.systemName))
            {
                AddNewTask(el.systemName,el.valueNeeded);
            }

        }

    }

    public void AddNewTask(string _systemName,int _value)
    {
        if (GetOutstandingSystems().Contains(_systemName) == false) 
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
            
            if(GetOutstandingSystems().Contains(_system.GetSystemName()))
            {GetOutstandingSystems().Remove(_system.GetSystemName());}

            CheckTaskComplete();
        }

    }

    public void CheckTaskComplete()
    {
        if(GetOutstandingSystems().Count == 0)
        {
                GetTaskSelector().currentTaskCount++;
                SelectNewTaskGroup();
        }

    }


    public void StartGame()
    {
        if(gameOn ==  true  ){return;}

        gameOn = true;

SelectNewTaskGroup();
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

    public TaskSelector GetTaskSelector()
    {
        if(taskSelector == null){taskSelector = GetComponent<TaskSelector>();}
        return taskSelector;
    }
    private EventManager eventManager;
 public EventManager GetEventManager()
    {
        if(eventManager == null){eventManager = GetComponent<EventManager>();}
        return eventManager;
    }
}
