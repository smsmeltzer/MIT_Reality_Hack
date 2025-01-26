using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TaskObject")]
public class TaskObject : ScriptableObject
{
    public string taskCategory;

    public List<TaskSubObject> tasks;
}

[System.Serializable]
public class TaskSubObject
{
    public string systemName;
    public int valueNeeded;

}

