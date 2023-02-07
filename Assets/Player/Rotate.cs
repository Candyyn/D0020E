using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VivoxUnity;

public class Rotate : NetworkBehaviour
{
    public GameObject box;

    private Camera cam;

    private VivoxVoiceManager _vivoxVoiceManager;

    private float _nextPosUpdate = 0;
    // Start is called before the first frame update


    private bool isOwner => GetComponent<NetworkObject>().IsOwner;

    // Update is called once per frame
    // send RCP to server to update color and rotation


    private void Awake()
    {
        if (!isOwner) return;
        _vivoxVoiceManager = VivoxVoiceManager.Instance;
    }


    private void Start()
    {
        if (!isOwner) return;
        cam = Camera.main;
    }

    private void Update()
    {
        if (!isOwner) return;

        // Change color over time
        //box.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
        // rotate slowly over time
        //transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        box.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
        Vector3 eulerRotation = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
        CmdRotateBoxServerRpc(eulerRotation);


        if (!(Time.time > _nextPosUpdate) || _vivoxVoiceManager.LoginState != LoginState.LoggedIn) return;
        Update3DPosition(gameObject.transform, cam.transform);
        _nextPosUpdate += 0.3f;
    }

    private void Update3DPosition(Transform listener, Transform speaker)
    {
        // _cachedPosition is a custom class with a method that checks the given values against its cached versions. If they differ, it returns true and caches the given ones.
        _vivoxVoiceManager.TransmittingSession.Set3DPosition(speaker.position, listener.position,
            listener.forward, listener.up);
    }


    [ServerRpc(RequireOwnership = true)]
    void CmdRotateBoxServerRpc(Vector3 rotation)
    {
        //transform.rotation = cam.transform.rotation;
        //transform.Rotate(rotation);
        transform.rotation = Quaternion.Euler(rotation);

        // rotate slowly over time
        //transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
}