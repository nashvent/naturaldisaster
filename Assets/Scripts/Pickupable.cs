using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SocketIO;

public class Pickupable : MonoBehaviour {
    public SocketIOComponent socket;

    public bool capturado = false;
    public bool creado = false;
    public GameObject[] all_objects;
    int id_server;

    // Use this for initialization
    void Start () {
        all_objects = GameObject.FindGameObjectsWithTag("box");
        
        //Agrego para que sea pickable
        EventTrigger eventTrigger1 = this.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { capturar(); });
        eventTrigger1.triggers.Add(entry);

        //socket
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On("omovimiento", drawMovimiento);
        socket.On("ocrear", createObjet);

    }

    // Update is called once per frame
    void Update () {
        if (capturado)
        {
            StartCoroutine(updatePosition());
            copyPosition();
        }
        /*if (!creado)
        {
            objetos_juntos();
        }*/
    }

    public void copyPosition()
    {
        var pos = GameObject.Find("Head").transform.position;
        //objeto = GameObject.Find(this.gameObject.name);
        this.gameObject.transform.position = new Vector3(pos[0],pos[1],pos[2]+3);        
    }

    public void capturar()
    {
        capturado = !capturado;
        if (!capturado)
        {
            Debug.Log("no estoy capturado");
            this.gameObject.transform.position= new Vector3(
                this.gameObject.transform.position.x,
                this.gameObject.transform.position.y-1,
                this.gameObject.transform.position.z
                ); 
        }
    }
    /*
    public void objetos_juntos()
    {
        Vector3 res = Vector3.zero;
        if(all_objects.Length > 1)
        {
            foreach (GameObject obj in all_objects)
            {
                res = obj.transform.position - res;
            }
            var di = Vector3.Distance(res,Vector3.zero);
            if (di <= 2)
            {
               createObjet();
            }
        }
    }*/

    public void createObjet(SocketIOEvent e)
    {
        var pos = e.data.GetField("pos");
        Debug.Log("Voy a crear mi obj");
        Debug.Log(pos);
        Vector3 ultima_pos = new Vector3(float.Parse(pos.GetField("x").ToString()), float.Parse(pos.GetField("y").ToString()), float.Parse(pos.GetField("z").ToString()));

        //Vector3 ultima_pos = this.transform.position;
        //ultima_pos.y = ultima_pos.y - 1;
        foreach (GameObject obj in all_objects)
        {
            Debug.Log("Estoy destruyendo");
            Destroy(obj);
        }
        //new_obj.SetActive(true);
        Object prefab = Resources.Load("boat"); // Assets/Resources/Prefabs/prefab1.FBX
        GameObject cube = (GameObject)Instantiate(prefab, ultima_pos, Quaternion.identity);
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.AddComponent<PickNew>();
        //cube.transform.position = ultima_pos;
        Debug.Log("OBJETO CREADO");
        //creado = true;
              
    }

    IEnumerator updatePosition()
    {
        yield return new WaitForSeconds(0.5f);
        JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
        var pos = this.gameObject.transform.position;
        j.AddField("x", pos.x);
        j.AddField("y", pos.y);
        j.AddField("z", pos.z);
        j.AddField("id", id_server);
        socket.Emit("oposition", j);
    }

    public void setIdserver(int val)
    {
        id_server = val;
        Debug.Log(id_server);
    }

    public void drawMovimiento(SocketIOEvent e)
    {
        //Debug.Log(string.Format("[ data: {0}]", e.data["clients"]));
        var listObj = e.data.GetField("objetos");
        Debug.Log("recibo player 1");
        for (int i = 0; i < listObj.list.Count; i++)
        {
            JSONObject objData = (JSONObject)listObj.list[i];
            var pos = objData.GetField("position");
            all_objects[i].transform.position = new Vector3(float.Parse(pos.GetField("x").ToString()), float.Parse(pos.GetField("y").ToString()), float.Parse(pos.GetField("z").ToString()));
        }
        
    }
}
