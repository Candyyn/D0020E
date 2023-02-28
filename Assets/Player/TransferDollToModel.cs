using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferDollToModel : MonoBehaviour
{
    public GameObject doll;
    public GameObject Avatar;
    public GameObject model;


    private void Awake()
    {
        Avatar = gameObject;
        doll = transform.parent.gameObject;
        model = GameObject.FindWithTag("PlayerHead");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = model.transform.GetChild(0).transform.rotation;
    }
}