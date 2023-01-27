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
    
    
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    private Random rnd = new Random();

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            // Press T to spawn set object. 
            // Spawns object using the networkObject in order to sync between the server and clients. 
            // Important to note that the host act as a client and server at the same time.
            // Only the host is allowed to spawn things. This does not allow the client to spawn network objects.
            
            spawnedObjectTransform = Instantiate(spawnedObjectPrefab);
            spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);

        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            //Press "Y" to destroy.
            //This does not work on the client side so only the server can destroy spawned network objects!
            spawnedObjectTransform.GetComponent<NetworkObject>().Despawn(true);
            Destroy(spawnedObjectTransform.gameObject);
        }

        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;

        float moveSpeed = 3f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

    }

    // Use ServerRpc to communicate with eachother in chat? Might be big pog
    [ServerRpc]
    private void TestServerRpc()
    {
        // Do something here xd
    }

}
