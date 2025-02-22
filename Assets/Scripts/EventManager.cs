using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public class EventManager : MonoBehaviourPun
{
    public List<UnityEvent> tutorialEvents = new List<UnityEvent>();
    public List<UnityEvent> gameEvents = new List<UnityEvent>();
    
    [SerializeField] AudioSource AstronautAudioSource;
    [SerializeField] AudioSource CommandAudioSource;
    [SerializeField] TextMeshProUGUI AstronautText;
    [SerializeField] TextMeshProUGUI CommandText;

    public bool astronautReady = false;
    public bool commandReady = false;
    public bool taskCompleted = true;

    public AudioClip clip1;
    public AudioClip clip2;

    private Timer timer;
    private TaskManager taskManager;
    private int index = 0;
    private bool tutorial = true;
    private bool game = false;
       

    private void Start()
    {
        taskManager = GetComponent<TaskManager>();
        timer = GetComponent<Timer>();
    }

    [SerializeField] private ShipSystemManager shipSystemManager;
    private void ReadyCheck()
    {
        if (astronautReady && commandReady && taskCompleted)
        {
            photonView.RPC("RPCNextLine", RpcTarget.All);
        }
    }
#region


    public void TryUpdateTask(int _type,int _index)
    {

       if(PhotonNetwork.IsMasterClient == false){return;}
        photonView.RPC("RPCSyncTask" ,RpcTarget.All,_type,_index);

    }

    [PunRPC]
    public void RPCSyncTask(int _type,int _index)
    {
      
        SyncTask(_type,_index);
    }

    public void SyncTask(int _type,int _index)
    {
        GetTaskManager().SynceNewTask(_type,_index);

    }


    public void TryUpdateSystem(ShipSystem _system)
    {
        //called when a player changes an interactable on the ship
        //intended to then be passed as an rpc to all players to sync their values

        photonView.RPC("RPCSyncShipSystem" ,RpcTarget.All,_system.GetSystemName(),_system.GetSystemValue());

    }

    [PunRPC]
    public void RPCSyncShipSystem(string _systemName,int _value)
    {
        if (tutorial) { 
            tutorialEvents[index].Invoke();
        }
        else if (game)
        {
            gameEvents[index].Invoke();
        }
        index++;

        SyncShipSystem(_systemName,_value);
    }

    public void SyncShipSystem(string _name,int _value)
    {
        GetShipSystemManager().UpdateShipSystem(_name,_value);
        GetTaskManager().CheckSystem(GetShipSystemManager().GetSystemObjects()[_name]);
    }

    public ShipSystemManager GetShipSystemManager()
    {
        if(shipSystemManager == null)
        {shipSystemManager = FindObjectOfType<ShipSystemManager>(); }
        return shipSystemManager;
    }
    public TaskManager GetTaskManager()
    {
        if(taskManager == null)
        {taskManager = FindObjectOfType<TaskManager>(); }
        return taskManager;
    }
#endregion




    [PunRPC]
    public void RPCNextLine()
    {
        if (tutorial) { 
            tutorialEvents[index].Invoke();
        }
        else if (game)
        {
            gameEvents[index].Invoke();
        }
        index++;
    }

    public void CommandReady()
    {
        commandReady = true;
        if (!astronautReady)
        {
            CommandText.text = "Waiting on Astronaut...";
        }
        ReadyCheck();
    }
    public void CommandNotReady()
    {
        commandReady = false;
    }
    public void AstronautReady()
    {
        astronautReady = true;
        if(!commandReady)
        {
            AstronautText.text = "Waiting on Mission Control...";
        }
        ReadyCheck();
    }
    public void AstronautNotReady()
    {
        astronautReady = false;
    }
    public void TaskCompleted()
    {
        taskCompleted = true;
        ReadyCheck();
    }
    public void TaskNotCompleted()
    {
        taskCompleted = false;
    }
    public void PlayAudioForAstronaut(AudioClip clip)
    {
        AstronautAudioSource.clip = clip;
        AstronautAudioSource.Play();
    }

    public void PlayAudioForCommand(AudioClip clip)
    {
        CommandAudioSource.clip = clip;
        CommandAudioSource.Play();
    }

    public void ChangeAstronautText(string text)
    {
        AstronautText.text = text;
    }

    public void ChangeCommandText(string text)
    {
        CommandText.text = text;
    }

    public void StartGame()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        PlayAudioForCommand(clip1);
        PlayAudioForAstronaut(clip1);
        yield return new WaitForSeconds(10);

        PlayAudioForCommand(clip2);
        PlayAudioForAstronaut(clip2);
        index = 0;
        game = true;
        tutorial = false;
        timer.StartTimer();
        taskManager.StartGame();
        ChangeAstronautText("");
        ChangeCommandText("");
    }

    public void EndGame()
    {
        if (taskManager.GetOutstandingSystems().Count == 0) // success
        {         
            ChangeCommandText("You Win! The Astronauts landed on the moon.");
            ChangeAstronautText("You Win! The Astronauts landed on the moon.");
        }
        else    // failure
        {
            ChangeCommandText("Mission Aborted. The spacecraft has crashed on the moon.");
            ChangeAstronautText("Mission Aborted. The spacecraft has crashed on the moon.");
        }
    }



}
