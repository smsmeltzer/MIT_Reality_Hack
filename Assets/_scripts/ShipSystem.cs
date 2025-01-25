using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipSystem : MonoBehaviour
{
    private TaskManager taskManager;

    [SerializeReference] private string systemName;
    [SerializeReference] private GameObject warningLight;
    [SerializeReference] private TextMeshPro displayText;
 [SerializeReference] private TextMeshPro systemNameText;
    [SerializeReference] private Transform grabber;
    
    [SerializeReference] private int testingInterger;



    // Start is called before the first frame update
    void Start()
    {
        if(systemNameText != null){systemNameText.SetText(systemName);}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckSystem()
    {


    }

    void OnMouseDown()
    {
        testingInterger++;
        if(testingInterger > 2){testingInterger = 0;}
        GetDisplayText().SetText(testingInterger.ToString());
    }

    public TextMeshPro GetDisplayText()
    {
        return displayText;

    }

    public GameObject GetWarningLight()
    {
        return warningLight;

    }

    public string GetSystemName()
    {
        return systemName;

    }

    public TaskManager GetTaskManager()
    {
        if(taskManager == null)
        {taskManager = FindObjectOfType<TaskManager>(); }
        return taskManager;
    }
}
