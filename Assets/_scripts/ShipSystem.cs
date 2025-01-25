using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ShipSystemValueType{binary,range,exact}

public class ShipSystem : MonoBehaviour
{
    private TaskManager taskManager;

    [SerializeReference] private string systemName;
    [SerializeReference] private int currentValue;
    [SerializeReference] private GameObject warningLight;
    [SerializeReference] private TextMeshPro displayText;
    [SerializeReference] private TextMeshPro systemNameText;
    [SerializeReference] private ShipSystemValueType valueType;
    
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

    public bool CheckSystem(int _valueNeeded)
    {
        if(GetSystemValueType() == ShipSystemValueType.binary)
        {
            if(_valueNeeded >= 50){return GetSystemValue() >= 50;}
            return GetSystemValue() < 50;
        }

        if(GetSystemValueType() == ShipSystemValueType.exact)
        {
            return GetSystemValue() == _valueNeeded;
        }

        return Mathf.Abs(_valueNeeded - GetSystemValue()) <= 25;
    }

    public void SetSystemValue(int _newValue)
    {
        currentValue = _newValue;

    }
    public int GetSystemValue( )
    {
        return currentValue ;

    }

    public void DisplaySystemData(int _value)
    {
        string displayString = "--";
        if(GetSystemValueType() == ShipSystemValueType.binary)
        {
            if(_value >= 50){displayString = "ON";}
            else{displayString = "OFF";}
            
        }

        if(GetSystemValueType() == ShipSystemValueType.exact)
        {
           displayString = _value.ToString() + "%";
        }

        if(GetSystemValueType() == ShipSystemValueType.range)
        {
            if(_value > 75)
            {
                displayString = "-HIGH";
            }else if (_value < 25){ displayString =  "LOW--";}
            else{displayString = "-MED-";}
        }

        GetDisplayText().SetText(displayString);
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

    public ShipSystemValueType GetSystemValueType()
    {

        return valueType;
    }

    public TaskManager GetTaskManager()
    {
        if(taskManager == null)
        {taskManager = FindObjectOfType<TaskManager>(); }
        return taskManager;
    }
}
