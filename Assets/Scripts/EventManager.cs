using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public class EventManager : MonoBehaviourPun
{

    public enum EventTypes
    {
        Undocking, Angle, Velocity, LifeSupport, Landing
    }

    public List<UnityEvent> tutorialEvents = new List<UnityEvent>();
    public List<UnityEvent> gameEvents = new List<UnityEvent>();
    
    [SerializeField] AudioSource AstronautAudioSource;
    [SerializeField] AudioSource CommandAudioSource;
    [SerializeField] TextMeshProUGUI AstronautText;
    [SerializeField] TextMeshProUGUI CommandText;

    public bool astronautReady = false;
    public bool commandReady = false;
    public bool taskCompleted = true;

    private int index = 0;
    private bool tutorial = true;
    private bool game = false;

  [SerializeField] private ShipSystemManager shipSystemManager;
    private void ReadyCheck()
    {
        if (astronautReady && commandReady && taskCompleted)
        {
            photonView.RPC("RPCNextLine", RpcTarget.All);
        }
    }
#region

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
    }

    public void SyncShipSystem(string _name,int _value)
    {
        GetShipSystemManager().UpdateShipSystem(_name,_value);

    }

        public ShipSystemManager GetShipSystemManager()
    {
        if(shipSystemManager == null)
        {shipSystemManager = FindObjectOfType<ShipSystemManager>(); }
        return shipSystemManager;
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
            CommandText.text = "Waiting on Astronauts...";
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
        yield return new WaitForSeconds(5);
        index = 0;
        game = true;
        tutorial = false;
    }

    public void EndGame()
    {

    }


}
