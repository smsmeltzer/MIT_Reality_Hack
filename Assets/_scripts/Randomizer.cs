using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Randomizer : MonoBehaviour
{
        public TextMeshPro taskTxt;
        private TaskManager taskManager;

        private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 5)
        {
            timer = 0;
            if(GetTaskManager().GetOutstandingSystems().Count == 0)
            {
                RandomizeTasks();
            }
        }
    }

    public void RandomizeTasks()
    {
        string newTask = "";
        int newValue = (int)Random.Range(-10,10);
        GetTaskManager().AddNewTask("slide_LR", newValue);
        newTask += "slide_LR " + newValue;

        //  newValue = (int)Random.Range(-10,10);
        // GetTaskManager().AddNewTask("DIAL", newValue);
        // newTask += "\nDIAL " + newValue;
        taskTxt.SetText(newTask);

    }

    public TaskManager GetTaskManager()
    {
        if(taskManager == null)
        {taskManager = FindObjectOfType<TaskManager>(); }
        return taskManager;
    }
}
