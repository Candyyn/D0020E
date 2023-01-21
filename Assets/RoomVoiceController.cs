using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomVoiceController : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// All parameters for the Controller
    /// </summary>
    ///
    
    private Photon.Voice.Unity.Recorder Recorder;
    private Photon.Voice.PUN.PhotonVoiceView VoiceView;
    [Header("Voice Info")]
    public bool isConnectedToMaster = false;
    public float voiceAMP = 0.0f;
    
    [Header("UI Variables")]
    
    public Button JoinButton, CreateButton;
    public TMPro.TextMeshProUGUI RoomNameText, IsTalkingText, VoiceConnectedText;


    // Start is called before the first frame update
    void Start()
    {
        Recorder = GetComponent<Photon.Voice.Unity.Recorder>();
        VoiceView = FindObjectOfType<Photon.Voice.PUN.PhotonVoiceView>();
        // Connect to the Photon Master Server
        PhotonNetwork.ConnectUsingSettings();
        
        CreateButton.onClick.AddListener(() => {
            CreateVoiceRoom("Room 1");
        });
        JoinButton.onClick.AddListener(() => {
            JoinLeaveRoom("Room 1");
        });

    }
    
    // Update is called once per frame
    void Update()
    {
        voiceAMP = VoiceView.RecorderInUse.LevelMeter.CurrentPeakAmp;
        if(voiceAMP > 0.01f)
        {
            IsTalkingText.text = "Talking: True";
        }
        else
        {
            IsTalkingText.text = "Talking: False";
        }
    }
    
    
    
    /// <summary>
    /// Get the current voice room
    /// </summary>
    /// <returns>Voice Room ID</returns>
    public string getCurrentRoom()
    {
        return PhotonNetwork.CurrentRoom.Name;
    }
    
    /// <summary>
    /// Create a Voice Room
    /// </summary>
    /// <param name="roomID"></param>
    /// <returns></returns>
    public void CreateVoiceRoom(string roomID)
    {
        if(!isConnectedToMaster)
        {
            Debug.Log("Not connected to Master");
            return;
        }
        PhotonNetwork.CreateRoom(roomID, new Photon.Realtime.RoomOptions { MaxPlayers = 10 });
    }
    
    
    /// <summary>
    /// Event for Client connect to the Master Voice Server
    /// </summary>
    /// <returns></returns>
    public override void OnConnectedToMaster()
    {
        isConnectedToMaster = true;
        Debug.Log("Connected to Master");
    }
    
    /// <summary>
    /// Event for when creating a room fails
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public override void OnCreateRoomFailed(short returnCode, string message) => Debug.Log("Room Creation Failed:" + message);

    /// <summary>
    /// Event for when creating a room succeeds
    /// </summary>
    /// <returns></returns>
    public override void OnJoinedRoom()
    {
        RoomNameText.text = getCurrentRoom();
        VoiceConnectedText.text = "Voice: Connected";
        JoinButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Leave Room";
    }

    public override void OnLeftRoom()
    {
        VoiceConnectedText.text = "Voice: Disconnected";
        JoinButton.GetComponentInChildren<TMPro.TMP_Text>().text = "Join Room";
    }

    /// <summary>
    /// Join A Voice Room
    /// </summary>
    /// <param name="roomID"></param>
    /// <returns></returns>
    public void JoinVoiceRoom(string roomID)
    {
        if(!isConnectedToMaster)
        {
            Debug.Log("Not connected to Master");
            return;
        }
        PhotonNetwork.JoinRoom(roomID);
    }


    private void JoinLeaveRoom(string roomID)
    {

        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            PhotonNetwork.JoinRoom(roomID);
        }
    }
}
