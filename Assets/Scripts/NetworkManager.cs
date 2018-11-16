﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;


public class NetworkManager : MonoBehaviour {
    public SocketIOComponent socket;
    public GameObject cube;
    // Use this for initialization
    void Start () {
        StartCoroutine(ConnectToServer());
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(3, 3, 3);
        socket.On("movimiento", TestBoop);
    }
	
    IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(0.5f);
        socket.Emit("player connect");
    }
	// Update is called once per frame
	void Update () {
        StartCoroutine(updatePosition());
        //StartCoroutine(messageServer());
    }

    IEnumerator updatePosition()
    {
        yield return new WaitForSeconds(0.5f);
        JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
        var pos = GameObject.Find("Head").transform.position;

        j.AddField("x", pos.x);
        j.AddField("y", pos.y);
        j.AddField("z", pos.z);
        
        socket.Emit("position",j);

    }

    public void TestBoop(SocketIOEvent e)
    {
        //Debug.Log("Ando reviciendo");
        Debug.Log(string.Format("[ data: {0}]", e));
    }

    IEnumerator messageServer()
    {
        yield return new WaitForSeconds(0.5f);
        socket.On("conectado", TestBoop);
        Debug.Log("pasnando mssagver");
    }


}
    