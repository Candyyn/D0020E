using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FollowHand : NetworkBehaviour
{
    public string handName;
    public bool canSeeHand = false;
    private bool isReset = true;
    private bool isOwner => GetComponent<NetworkObject>().IsOwner;

    // Start is called before the first frame update
    private void Start()
    {
        if (!isOwner || !IsLocalPlayer) return;
    }


    // Update is called once per frame
    private void Update()
    {
        if (!isOwner) return;
        var hand = GameObject.Find(handName);
        if (hand == null)
        {
            canSeeHand = false;
            
            if (isReset) return;
            isReset = true;
            CmdRotateBoxServerRpc(new Vector3(0, 0, 0));
            return;
        }

        canSeeHand = true;
        isReset = false;
        Vector3 handPos = new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z);
        CmdRotateBoxServerRpc(handPos);
    }


    /*
     * Send RPC to server to update hand position
     */
    [ServerRpc(RequireOwnership = true)]
    private void CmdRotateBoxServerRpc(Vector3 position)
    {
        transform.position = position;
        //transform.rotation = Quaternion.Euler(rotation);
    }
}