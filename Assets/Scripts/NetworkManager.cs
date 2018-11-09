using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkManager : MonoBehaviour {
    public SocketIOComponent socket;

	// Use this for initialization
	void Start () {
        StartCoroutine(ConnectToServer());	
	}
	
    IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(0.5f);
        socket.Emit("player connect");
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
