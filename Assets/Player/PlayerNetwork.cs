using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Netcode;
using UnityEngine;
using Random = System.Random;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private Transform spawnedObjectPrefab;
    private Transform spawnedObjectTransform; // Used in Update function.
    [SerializeField] private Transform Head;
    [SerializeField] private Transform L_Hand;
    [SerializeField] private Transform R_Hand;

    private GameObject headInstance;
    private GameObject R_HandInstance;
    private GameObject L_HandInstance;

    //private GameObject hololens;
    private Camera cam;


    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    private Random rnd = new Random();

    private void Start()
    {
        if (!IsLocalPlayer) return;
        cam = Camera.main;
    }

    private void Update()
    {
        if (!IsOwner) return;
        transform.position = cam.transform.position;
    }

    // Use ServerRpc to communicate with eachother in chat? Might be big pog


    private GameObject InstantiateObject(GameObject obj, ulong owner, GameObject parent = null)
    {
        var instance = Instantiate(obj);
        instance.GetComponent<NetworkObject>().SpawnWithOwnership(owner);
        instance.transform.localPosition = new Vector3(0, 0, 0);
        if (parent != null)
        {
            instance.transform.SetParent(parent.transform);
        }
        return obj;
    }


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsHost || IsServer)
        {
            headInstance = InstantiateObject(Head.gameObject, OwnerClientId, gameObject);
            R_HandInstance = InstantiateObject(R_Hand.gameObject, OwnerClientId, gameObject);
            L_HandInstance = InstantiateObject(L_Hand.gameObject, OwnerClientId, gameObject);
        }


        if (!IsOwner || !IsLocalPlayer) return;
        // disable renderer for all children
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }


        // Find game object with that "MRKT" tag and set it as parent.
        //GameObject.FindWithTag("MRKT").transform.SetParent(gameObject.transform);
    }
}