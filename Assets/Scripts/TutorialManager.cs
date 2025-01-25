using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviourPun
{
    public List<UnityEvent> tutorialEvents = new List<UnityEvent>();
    [SerializeField] AudioSource AstronautAudioSource;
    [SerializeField] AudioSource CommandAudioSource;
    public bool astronautReady = false;
    public bool commandReady = false;
    public bool taskCompleted = true;

    private int index = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (astronautReady && commandReady && taskCompleted)
        {
            photonView.RPC("RPCNextLine", RpcTarget.All);
        }
    }

    [PunRPC]
    public void RPCNextLine()
    {
        index++;
        tutorialEvents[index].Invoke();
    }

    public void CommandReady()
    {
        commandReady = true;
    }
    public void CommandNotReady()
    {
        commandReady = false;
    }
    public void AstronautReady()
    {
        astronautReady = true;
    }
    public void AstronautNotReady()
    {
        astronautReady = false;
    }
    public void TaskCompleted()
    {
        taskCompleted = true;
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

}
