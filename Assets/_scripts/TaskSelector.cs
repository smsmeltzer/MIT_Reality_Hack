using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSelector : MonoBehaviour
{
    public int totalTasksPerGame = 7;
    public int currentTaskCount;

    public List<TaskObject> detaching;
    public List<TaskObject> angleAdjust;
    public List<TaskObject> velocityAdjust;
    public List<TaskObject> lifeSupport;
    public List<TaskObject> landing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame(int _taskCount=7)
    {
        totalTasksPerGame = _taskCount;

    }

    public int GetNextTaskIndex(int _taskCount)
    {
        //server gerts the index to broadcast to all players
        List<TaskObject> taskList = detaching;
        if(_taskCount == totalTasksPerGame)
        {
            taskList = landing;

        }else if (_taskCount == 0)
        {
             taskList = detaching;
        }else if (_taskCount % 2 == 0)
        {
             taskList = angleAdjust;
        }else if (_taskCount % 3 == 0)
        {
             taskList = velocityAdjust;
        }else{ taskList = lifeSupport;}

        return (int)Random.Range(0,taskList.Count);

    }

     public TaskObject GetNextTask(int _type,int _index)
    {
        //called on the client for syncing the specific task
        List<TaskObject> taskList = detaching;
        if(_type == totalTasksPerGame)
        {
            taskList = landing;

        }else if (_type == 0)
        {
             taskList = detaching;
        }else if (_type % 2 == 0)
        {
             taskList = angleAdjust;
        }else if (_type % 3 == 0)
        {
             taskList = velocityAdjust;
        }else{ taskList = lifeSupport;}

        if(taskList.Count > _index)
        {return taskList[_index];}

        return taskList[taskList.Count - 1];

    }

    public List<TaskObject> GetTaskList(int _type)
    {
        if(_type == 0)
        {
            if(detaching == null){detaching = new List<TaskObject>();}
            return detaching;
        }

        if(_type == 1)
        {
            if(angleAdjust == null){angleAdjust = new List<TaskObject>();}
            return angleAdjust;
        }

        if(_type == 2)
        {
            if(velocityAdjust == null){velocityAdjust = new List<TaskObject>();}
            return velocityAdjust;
        }

        if(_type == 3)
        {
            if(lifeSupport == null){lifeSupport = new List<TaskObject>();}
            return lifeSupport;
        }

        if(_type == 4)
        {
            if(landing == null){landing = new List<TaskObject>();}
            return landing;
        }

        return angleAdjust;
    }

}
