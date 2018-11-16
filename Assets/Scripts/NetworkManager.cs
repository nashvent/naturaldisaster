using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Globalization;

public class NetworkManager : MonoBehaviour {
    public SocketIOComponent socket;
    
    public List<GameObject> GamePlayers;
    float contPos = 0;
    //public GameObject cube;
    // Use this for initialization
    void Start () {
        StartCoroutine(ConnectToServer());
        socket.On("pmovimiento", drawMovimiento);
        for (int i = 0; i < 4; i++)
        {
            GameObject nG= GameObject.CreatePrimitive(PrimitiveType.Cube);
            GamePlayers.Add(nG);
            GamePlayers[i].transform.position = new Vector3(3+i, 3, 3+i);
        }
    }
	
    IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(0.5f);
        socket.Emit("player connect");
    }
	// Update is called once per frame
	void Update () {
        StartCoroutine(updatePosition());
    }

    IEnumerator updatePosition()
    {
        yield return new WaitForSeconds(0.5f);
        JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
        var pos = GameObject.Find("Head").transform.position;
        j.AddField("x", pos.x);
        j.AddField("y", pos.y);
        j.AddField("z", pos.z);
        socket.Emit("cposition",j);
    }



    public void drawMovimiento(SocketIOEvent e)
    {
        //Debug.Log(string.Format("[ data: {0}]", e.data["clients"]));
        var listClients=e.data.GetField("nclients");
        for (int i = 0; i < listClients.list.Count; i++)
        {
            //Debug.Log("Player");
            JSONObject playerData = (JSONObject)listClients.list[i];
            var pos = playerData.GetField("position");
            GamePlayers[i].transform.position = new Vector3( float.Parse( pos.GetField("x").ToString()), float.Parse(pos.GetField("y").ToString()), float.Parse(pos.GetField("z").ToString()));
            //Debug.Log(playerData);
            // Process the player key and data as you need.
        }
      
    }



}
    