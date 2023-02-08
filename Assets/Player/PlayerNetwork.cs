using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using ReadyPlayerMe;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Random = System.Random;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private Transform spawnedObjectPrefab;
    private Transform spawnedObjectTransform; // Used in Update function.
    [SerializeField] private Transform Head;
    [SerializeField] private Transform L_Hand;
    [SerializeField] private Transform R_Hand;

    public GameObject avatar;
    public GameObject child;

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


        var avatarLoader = new AvatarLoader();
        avatarLoader.OnCompleted += (_, args) =>
        {
            //Debug.Log("Avatar loaded"); 
            avatar = args.Avatar;
            //AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, avatar);

            avatar.transform.SetParent(gameObject.transform);


            //Find a Child Game Object of Avatar called "Neck" and add a LimitChildRotation component to it.

            Debug.Log(avatar.transform.Find("Neck"));


            child = RecursiveFindChild(avatar.transform, "Neck").gameObject;
            var head = RecursiveFindChild(child.transform, "Head").gameObject;
            //child = GameObject.Find("Neck");
            child.AddComponent<LimitChildRotation>().child = head;
            head.AddComponent<TransferDollToModel>();
        };
        avatarLoader.LoadAvatar("https://models.readyplayer.me/63e3d13092545d144f7bff4e.glb");


        if (!IsOwner || !IsLocalPlayer) return;
        // disable renderer for all children
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }


        // Find game object with that "MRKT" tag and set it as parent.
        //GameObject.FindWithTag("MRKT").transform.SetParent(gameObject.transform);
    }

    Transform RecursiveFindChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }
            else
            {
                Transform found = RecursiveFindChild(child, childName);
                if (found != null)
                {
                    return found;
                }
            }
        }

        return null;
    }
}