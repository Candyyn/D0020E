using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Netcode;
using VivoxUnity;

public class NetworkStarter : MonoBehaviour
{
    VivoxVoiceManager voiceManager;
    public string channelName = "TestChannel";

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
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        Login();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        Login();
    }


    public void Login()
    {
        //voiceManager.Login(NetworkManager.Singleton.LocalClient.ClientId.ToString());
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