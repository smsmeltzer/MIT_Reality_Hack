using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum systemName { }

public class TaskManager : MonoBehaviour
{
    [SerializeReference] private Dictionary<string, int> systemDictionary;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<string, int> GetSystemDictionary()
    {

        if(systemDictionary == null)
        {systemDictionary  = new Dictionary<string, int>();}

        return systemDictionary;
    }
}
