using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Globalization;

public class NetworkManager : MonoBehaviour {
    public SocketIOComponent socket;
    
    public List<GameObject> GamePlayers;
    public List<GameObject> GameObjs;
    float contPos = 0;
    
    //public GameObject cube;
    // Use this for initialization
    void Start () {
        StartCoroutine(ConnectToServer());
        socket.On("pmovimiento", drawMovimiento);
        socket.On("rmet", drawMet);

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
        socket.On("crearobj", iniciarJuego);
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
        var listObj= e.data.GetField("objetos");
        //Debug.Log(listObj);
        for (int i = 0; i < listClients.list.Count; i++)
        {
            JSONObject playerData = (JSONObject)listClients.list[i];
            var pos = playerData.GetField("position");
            GamePlayers[i].transform.position = new Vector3( float.Parse( pos.GetField("x").ToString()), float.Parse(pos.GetField("y").ToString()), float.Parse(pos.GetField("z").ToString()));
        }
      
    }

    public void drawMet(SocketIOEvent e)
    {
        var listMet = e.data.GetField("metlist");
        for (int i = 0; i < listMet.list.Count; i++)
        {
            JSONObject playerData = (JSONObject)listMet.list[i];
            var pos = playerData.GetField("position");
            GameObject nG = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Material yourMaterial;
            yourMaterial = (Material)Resources.Load("Texturas/Meteomat");
            nG.GetComponent<Renderer>().material = yourMaterial;
            nG.AddComponent<fallScript>();
            nG.transform.position = new Vector3(float.Parse(pos.GetField("x").ToString()), float.Parse(pos.GetField("y").ToString()), float.Parse(pos.GetField("z").ToString()));

            GameObject ter = GameObject.Find("Terrain");
            editTerrain terrain = ter.GetComponent<editTerrain>();
            StartCoroutine(delay());

            terrain.raiseTerrain(nG.transform.position);
            //GamePlayers[i].transform.position = new Vector3(float.Parse(pos.GetField("x").ToString()), float.Parse(pos.GetField("y").ToString()), float.Parse(pos.GetField("z").ToString()));

        }

    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(10);
    }

    public void iniciarJuego(SocketIOEvent e)
    {
        var listObj = e.data.GetField("objetos");
        for (int i = 0; i < listObj.list.Count; i++)
        {
            JSONObject playerData = (JSONObject)listObj.list[i];
            var pos = playerData.GetField("position");
            GameObject nG = GameObject.CreatePrimitive(PrimitiveType.Cube);
            nG.tag = "box";

            var ptipo = playerData.GetField("tipo");
            int oTipo =int.Parse(ptipo.ToString());

            Material yourMaterial;
            if (oTipo==1)
            {
                yourMaterial = (Material)Resources.Load("Texturas/Mobj1");
                nG.GetComponent<Renderer>().material = yourMaterial;
            }
            if(oTipo==0)
            {
                yourMaterial = (Material)Resources.Load("Texturas/Mobj2");
                nG.GetComponent<Renderer>().material = yourMaterial;
            }
            nG.AddComponent<Pickupable>();

            Pickupable mscript = nG.GetComponent<Pickupable>();
            mscript.setIdserver(i);

            GameObjs.Add(nG);
            GameObjs[i].transform.position = new Vector3(float.Parse(pos.GetField("x").ToString()), float.Parse(pos.GetField("y").ToString()), float.Parse(pos.GetField("z").ToString()));
        }

    }
}
   