using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using Unity.Netcode;
using VivoxUnity;

public class NetworkStarter : MonoBehaviour
{
    VivoxVoiceManager voiceManager;
    public string channelName = "TestChannel";

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    public void Awake()
    {
        voiceManager = VivoxVoiceManager.Instance;
        voiceManager.OnUserLoggedInEvent += OnUserLoggedIn;
        voiceManager.OnUserLoggedOutEvent += OnUserLoggedOut;

        if (voiceManager.LoginState == LoginState.LoggedIn)
        {
            OnUserLoggedIn();
        }
    }

    public void Start()
    {
        voiceManager = VivoxVoiceManager.Instance;
        Debug.Log("NetworkStarter.Start()");
    }

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        //Button1
    }

    public void StartHost()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += login;
        NetworkManager.Singleton.StartHost();

        var buttonHelper = button2.GetComponent<ButtonConfigHelper>();
        buttonHelper.MainLabelText = "Host Started";
        // Remove click event
        //button2.GetComponent<Interactable>().OnClick.RemoveAllListeners();


        //Login();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += login;
        NetworkManager.Singleton.StartClient();
        
        var buttonHelper = button3.GetComponent<ButtonConfigHelper>();
        buttonHelper.MainLabelText = "Connected to host";
        //Login();
    }


    void login(ulong clientId)
    {
        if(voiceManager.LoginState == LoginState.LoggedIn)
        {
            return;
        }
        
        try
        {
            
            voiceManager.Login("Anuser_" + clientId.ToString());
        } catch (Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
    }

    public void Login()
    { 
        voiceManager.Login(NetworkManager.Singleton.LocalClient.ClientId.ToString());
    }
    


    public void JoinChannel()
    {
        voiceManager.JoinChannel(channelName, ChannelType.Positional, VivoxVoiceManager.ChatCapability.AudioOnly);
    }


    private void OnUserLoggedIn()
    {
        var lobbychannel = voiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == channelName);
        if ((voiceManager && voiceManager.ActiveChannels.Count == 0)
            || lobbychannel == null)
        {
            JoinChannel();
        }
        else
        {
            if (lobbychannel.AudioState == ConnectionState.Disconnected)
            {
                // Ask for hosts since we're already in the channel and part added won't be triggered.

                lobbychannel.BeginSetAudioConnected(true, true, ar => { Debug.Log("Now transmitting into lobby channel"); });
            }
        }
    }

    private void OnUserLoggedOut()
    {
        voiceManager.DisconnectAllChannels();
    }
}