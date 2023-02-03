using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

public class NetworkStarter : MonoBehaviour
{
    public void Start()
    {
        Debug.Log("NetworkStarter.Start()");
    }

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }
    
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
