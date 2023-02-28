using System;
using ReadyPlayerMe;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Unity.Networking.Transport;

public class PlayerNetwork : NetworkBehaviour
{
    private Transform _spawnedObjectTransform; // Used in Update function.
    [SerializeField] private Transform head;
    [SerializeField] private Transform lHand;
    [SerializeField] private Transform rHand;
    [SerializeField] private Transform HandTrackingController;

    public GameObject avatar;
    public GameObject child;

    public GameObject HeadInstance { get; private set; }
    public GameObject RHandInstance { get; private set; }
    public GameObject LHandInstance { get; private set; }
    private GameObject _handTrackingInstance;

    //private GameObject hololens;
    private Camera cam;


    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    // A Variable for user avatar url, everyone can read it. but only the owner or the server/host can write it.
    // Default value is https://models.readyplayer.me/63e3d13092545d144f7bff4e.glb
    [SerializeField] private NetworkVariable<FixedString128Bytes> _avatarUrl = new("https://models.readyplayer.me/63e3d13092545d144f7bff4e.glb", NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    [SerializeField] private NetworkVariable<FixedString128Bytes> _name = new("Player", NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);


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


    private static GameObject InstantiateObject(GameObject obj, ulong owner, GameObject parent = null)
    {
        var instance = Instantiate(obj);
        instance.GetComponent<NetworkObject>().SpawnWithOwnership(owner);
        instance.transform.localPosition = new Vector3(0, 0, 0);
        if (parent != null)
        {
            instance.transform.SetParent(parent.transform);
        }

        return instance;
    }


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsLocalPlayer)
        {
            if (IsHost || IsServer)
                setPlayerNameClientRpc("Owner");
            else
                setPlayerNameClientRpc("Player");
        }


        Debug.Log("OnNetworkSpawn for " + _name.Value);
        
        LoadAvatar();

        if (!IsOwner || !IsLocalPlayer) return;
        // disable renderer for all children
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }
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

    void FillChildrenArray(GameObject parent, ref GameObject[] childrenArray)
    {
        foreach (Transform child in parent.transform)
        {
            // Add the child object to the array
            childrenArray[childrenArray.Length - 1] = child.gameObject;
            Array.Resize(ref childrenArray, childrenArray.Length + 1);

            // Recursively fill the array with any children of this child
            if (child.childCount > 0)
            {
                FillChildrenArray(child.gameObject, ref childrenArray);
            }
        }
    }

    [ClientRpc]
    public void setPlayerNameClientRpc(string name)
    {
        _name.Value = name;
    }


    [ClientRpc]
    public void changeAvatarClientRpc(String url)
    {
        _avatarUrl.Value = url;
        //loadPlayerAvatar();
    }

    [ClientRpc]
    void LoadPlayerAvatarClientRpc()
    {
        // Check if avatar is already loaded and if so. deletes it
        if (avatar != null)
        {
            Destroy(avatar);
        }


        var avatarLoader = new AvatarLoader();
        avatarLoader.OnCompleted += (_, args) =>
        {
            //Debug.Log("Avatar loaded"); 
            avatar = args.Avatar;
            //AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, avatar);

            avatar.transform.SetParent(gameObject.transform);
            avatar.transform.localPosition = new Vector3(0, (float)-0.652, (float)-0.023);


            //Disable animator of avatar
            avatar.GetComponent<Animator>().enabled = false;


            avatar.name = IsLocalPlayer ? "LocalPlayerAvatar" : "PlayerAvatar_" + Guid.NewGuid().ToString();


            // Add Script Bone Renderer
            //BoneRenderer boneRenderer = avatar.AddComponent<BoneRenderer>();
            Transform armature = avatar.transform.Find("Armature");
            // Loop over all children
            GameObject[] allChildren = new GameObject[1];
            allChildren[0] = gameObject;
            FillChildrenArray(armature.gameObject, ref allChildren);

            /*try
            {
                boneRenderer.transforms = new Transform[allChildren.Length];
                for (int i = 0; i < allChildren.Length; i++)
                {
                    boneRenderer.transforms[i] = allChildren[i].transform;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }*/
            
            child = RecursiveFindChild(avatar.transform, "Neck").gameObject;

            var o = RecursiveFindChild(child.transform, "Head").gameObject;
            //child = GameObject.Find("Neck");
            //child.AddComponent<LimitChildRotation>().child = o;
            //o.AddComponent<TransferDollToModel>();
        };
        avatarLoader.LoadAvatar(_avatarUrl.Value.ToString());
    }


    void LoadAvatar()
    {
        if (IsLocalPlayer)
        {
            LoadPlayerAvatar();
            LoadConnectedPlayersAvatar();
        }
        else
        {
            LoadNetworkPlayerAvatar();
        }
    }

    void LoadPlayerAvatar()
    {
        if (avatar != null)
        {
            Destroy(avatar);
        }

        var avatarLoader = new AvatarLoader();

        avatarLoader.OnCompleted += (_, args) =>
        {
            //Debug.Log("Avatar loaded"); 
            avatar = args.Avatar;
            //AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, avatar);

            avatar.transform.SetParent(gameObject.transform);
            avatar.transform.localPosition = new Vector3(0, (float)-0.652, (float)-0.023);


            //Disable animator of avatar
            avatar.GetComponent<Animator>().enabled = false;


            avatar.name = IsLocalPlayer ? "LocalPlayerAvatar" : "PlayerAvatar_" + Guid.NewGuid().ToString();


            // Add Script Bone Renderer
            //BoneRenderer boneRenderer = avatar.AddComponent<BoneRenderer>();
            Transform armature = avatar.transform.Find("Armature");

            var hand1 = RecursiveFindChild(armature, "RightHand").gameObject.AddComponent<handController>();
            hand1.handProxy = rHand.gameObject;
            hand1.TagForHand = "RightHand";
            hand1.Init();

            var hand2 = RecursiveFindChild(armature, "LeftHand").gameObject.AddComponent<handController>();
            hand2.handProxy = lHand.gameObject;
            Debug.Log(lHand);
            hand2.TagForHand = "LeftHand";
            hand2.Init();


            child = RecursiveFindChild(avatar.transform, "Neck").gameObject;

            var o = RecursiveFindChild(child.transform, "Head").gameObject;
            //child = GameObject.Find("Neck");
            child.AddComponent<LimitChildRotation>().child = o;
            //o.AddComponent<TransferDollToModel>();
        };

        avatarLoader.LoadAvatar(_avatarUrl.Value.ToString());
    }

    void LoadNetworkPlayerAvatar()
    {
        if (avatar != null)
        {
            Destroy(avatar);
        }

        var avatarLoader = new AvatarLoader();

        avatarLoader.OnCompleted += (_, args) =>
        {
            //Debug.Log("Avatar loaded"); 
            avatar = args.Avatar;
            //AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, avatar);

            avatar.transform.SetParent(gameObject.transform);
            avatar.transform.localPosition = new Vector3(0, (float)-0.652, (float)-0.023);


            //Disable animator of avatar
            avatar.GetComponent<Animator>().enabled = false;
            
            Transform armature = avatar.transform.Find("Armature");

            var hand1 = RecursiveFindChild(armature, "RightHand").gameObject.AddComponent<NetworkHandController>();
            hand1.side = "Right";
            hand1.init();

            var hand2 = RecursiveFindChild(armature, "LeftHand").gameObject.AddComponent<NetworkHandController>();
            hand2.side = "Left";
            hand2.init();

            avatar.name = "PlayerAvatar_" + Guid.NewGuid().ToString();
            
        };

        avatarLoader.LoadAvatar(_avatarUrl.Value.ToString());
    }


    void LoadConnectedPlayersAvatar()
    {
        // Loop over all game objects with tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PlayerNetwork playerNetwork = player.GetComponent<PlayerNetwork>();
            if (playerNetwork != null && playerNetwork != this) // check if the playerNetwork exists and is not the current player
            {
                playerNetwork.LoadNetworkPlayerAvatar();
            }
        }
    }
}