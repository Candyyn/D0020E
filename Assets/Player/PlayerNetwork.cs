using System;
using System.Linq;
using Microsoft.MixedReality.Toolkit.UI;
using ReadyPlayerMe;
using Unity.Collections;
using Unity.Netcode;
using Unity.XR.CoreUtils;
using UnityEngine;


/// <summary>
/// A temporary enum to represent the different avatar models. In the future, this will be replaced by an single url that gets passed on between clients.
/// </summary>
public enum AvatarModel
{
    Female1 = 0,
    Female2 = 1,
    Female3 = 2,
    Male1 = 3,
    Male2 = 4,
    Male3 = 5
}


public class PlayerNetwork : NetworkBehaviour
{
    private Transform _spawnedObjectTransform; // Used in Update function.
    [SerializeField] private Transform head;
    [SerializeField] private Transform lHand;
    [SerializeField] private Transform rHand;
    [SerializeField] private Transform HandTrackingController;

    public GameObject Text;
    public GameObject avatar = null;
    public GameObject child;

    public GameObject HeadInstance { get; private set; }
    public GameObject RHandInstance { get; private set; }
    public GameObject LHandInstance { get; private set; }
    private GameObject _handTrackingInstance;


    public GameObject[] _buttonConfigHelpers = new GameObject[6];

    //private GameObject hololens;
    private Camera cam;

    // A Variable for user avatar url, everyone can read it. but only the owner or the server/host can write it.
    // Default value is https://models.readyplayer.me/63e3d13092545d144f7bff4e.glb
    private NetworkVariable<FixedString128Bytes> _avatarUrl =
        new("https://models.readyplayer.me/63e3d13092545d144f7bff4e.glb", NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

    private NetworkVariable<FixedString128Bytes> _name = new("Player", NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);


    private void Start()
    {
        if (!IsLocalPlayer) return;
        cam = Camera.main;

        GetSelectableButtons();
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
                SetPlayerNameClientRpc("Owner");
            else
                SetPlayerNameClientRpc("Player");
        }

        Debug.Log("Spawned " + _name.Value + " with id " + NetworkObjectId);

        LoadPlayerModel();

        _avatarUrl.OnValueChanged += OnAvatarUrlChanged;

        if (IsLocalPlayer && IsOwner)
            LoadConnectedPlayersAvatar();


        if (!IsOwner || !IsLocalPlayer) return;
        // disable renderer for all children
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }
    }

    private static Transform RecursiveFindChild(Transform parent, string childName)
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

    [ClientRpc]
    private void SetPlayerNameClientRpc(string userName)
    {
        _name.Value = userName;
    }

    //[ClientRpc]
    private void SetPlayerAvatar(string url)
    {
        _avatarUrl.Value = url;
    }

    /// <summary>
    /// Load the avatar of the network user
    /// </summary>
    public void LoadPlayerModel()
    {
        if (avatar != null)
            Destroy(avatar);

        AvatarLoader avatarLoader = new AvatarLoader();

        avatarLoader.OnCompleted += (_, args) =>
        {
            var allChildren = GetComponentsInChildren<Transform>();
            if (allChildren.Any(playerChildren => playerChildren.gameObject.name.Contains("PlayerAvatar_")))
            {
                Destroy(avatar);
            }


            // Avatar is loaded
            avatar = args.Avatar;
            // Reset position of avatar
            avatar.transform.SetParent(gameObject.transform);
            avatar.transform.localPosition = new Vector3(0, (float)-0.652, (float)-0.023);

            // Disable animator of avatar
            avatar.GetComponent<Animator>().enabled = false;

            avatar.name = IsLocalPlayer ? "LocalPlayerAvatar" : "PlayerAvatar_" + Guid.NewGuid().ToString();

            ImplementModel();
        };

        avatarLoader.LoadAvatar(_avatarUrl.Value.ToString());
    }


    /// <summary>
    /// Implement The model tracking scripts. Only being run by the owner of the object
    /// </summary>
    private void ImplementModel()
    {
        // Implement Hand Tracking
        var armature = avatar.transform.Find("Armature");

        if (IsOwner && IsLocalPlayer)
        {
            var hand1 = RecursiveFindChild(armature, "RightHand").gameObject.AddComponent<handController>();
            hand1.handProxy = rHand.gameObject;
            hand1.TagForHand = "RightHand";
            hand1.Init();

            var hand2 = RecursiveFindChild(armature, "LeftHand").gameObject.AddComponent<handController>();
            hand2.handProxy = lHand.gameObject;
            Debug.Log(lHand);
            hand2.TagForHand = "LeftHand";
            hand2.Init();
        }
        else
        {
            var hand1 = RecursiveFindChild(armature, "RightHand").gameObject.AddComponent<NetworkHandController>();
            hand1.side = "Right";
            hand1.init();

            var hand2 = RecursiveFindChild(armature, "LeftHand").gameObject.AddComponent<NetworkHandController>();
            hand2.side = "Left";
            hand2.init();
        }
    }

    private void LoadConnectedPlayersAvatar()
    {
        // Loop over all game objects with tag "Player"
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            var playerNetwork = player.GetComponent<PlayerNetwork>();
            if (playerNetwork != null && playerNetwork != this) // check if the playerNetwork exists and is not the current player
            {
                playerNetwork.LoadPlayerModel();
            }
        }
    }

    private void UpdateCharacterModel(AvatarModel model)
    {
        if (!IsOwner) return;

        // Female 1-3 and Male 1-3
        var url = model switch
        {
            AvatarModel.Female1 =>
                // Female 1
                "https://models.readyplayer.me/63fe24d496cb00e4ba398c60.glb",
            AvatarModel.Female2 =>
                // Female 2
                "https://models.readyplayer.me/63fe24eb96cb00e4ba398c78.glb",
            AvatarModel.Female3 =>
                // Female 3
                "https://models.readyplayer.me/63fe2504abec15b86171aec7.glb",
            AvatarModel.Male1 =>
                // Male 1
                "https://models.readyplayer.me/63fe249c9dc8b8dcb3b48565.glb",
            AvatarModel.Male2 =>
                // Male 2
                "https://models.readyplayer.me/63fe24aeabec15b86171ae5d.glb",
            AvatarModel.Male3 =>
                // Male 3
                "https://models.readyplayer.me/63fe24c296cb00e4ba398c47.glb",
            _ => "https://models.readyplayer.me/63e3d13092545d144f7bff4e.glb"
        };

        SetPlayerAvatar(url);
    }

    private void OnAvatarUrlChanged(FixedString128Bytes previousModel, FixedString128Bytes nextModel)
    {
        LoadPlayerModel();
    }

    private void GetSelectableButtons()
    {
        if (!IsLocalPlayer) return;
        //GameObject.Find("Female1").GetComponentsInParent<InteractableReceiverList>()
        _buttonConfigHelpers[0] = GameObject.Find("Female1");
        _buttonConfigHelpers[1] = GameObject.Find("Female2");
        _buttonConfigHelpers[2] = GameObject.Find("Female3");
        _buttonConfigHelpers[3] = GameObject.Find("Male1");
        _buttonConfigHelpers[4] = GameObject.Find("Male2");
        _buttonConfigHelpers[5] = GameObject.Find("Male3");

        for (var i = 0; i < _buttonConfigHelpers.Length; i++)
        {
            var model = (AvatarModel)i;
            _buttonConfigHelpers[i].GetComponent<ButtonConfigHelper>().OnClick.AddListener(() =>
            {
                var players = GameObject.FindGameObjectsWithTag("Player");
                foreach (var player in players)
                {
                    var playerNetwork = player.GetComponent<PlayerNetwork>();
                    if (playerNetwork == null || !playerNetwork.GetComponent<NetworkObject>().IsOwner) continue; // check if the playerNetwork exists and is not the current player
                    playerNetwork.UpdateCharacterModel(model);
                    break;
                }
            });
        }
    }
}