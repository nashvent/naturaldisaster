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
        
        /*EventTrigger eventTrigger1 = this.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { capturar(); });
        eventTrigger1.triggers.Add(entry);*/
    }
	
	// Update is called once per frame
	void Update () {
        if (capturado)
        {
            copyPosition();
        }
        if (!creado)
        {
       
            objetos_juntos();
        }
        //updatePosition();
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
    }

    public void createObjet()
    {
        Vector3 ultima_pos = this.transform.position;
        foreach (GameObject obj in all_objects)
        {
            Debug.Log("Estoy destruyendo");
            Destroy(obj);
        }
        //new_obj.SetActive(true);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.AddComponent<PickNew>();
        cube.transform.position = ultima_pos;
        Debug.Log("OBJETO CREADO");
        creado = true;
        /*EventTrigger eventTrigger1 = cube.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { capturar(); });
        eventTrigger1.triggers.Add(entry);*/
        
    }

    IEnumerator updatePosition()
    {
        yield return new WaitForSeconds(0.5f);
        JSONObject j = new JSONObject(JSONObject.Type.OBJECT);
        var pos = this.gameObject.transform.position;
        j.AddField("x", pos.x);
        j.AddField("y", pos.y);
        j.AddField("z", pos.z);
        socket.Emit("oposition", j);
    }

    void setIdserver(int val)
    {
        id_server = val;
    }
}
