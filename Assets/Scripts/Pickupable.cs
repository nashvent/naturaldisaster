using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pickupable : MonoBehaviour {

    public bool capturado = false;
    public bool creado = false;
    public GameObject[] all_objects;

    // Use this for initialization
    void Start () {
        all_objects = GameObject.FindGameObjectsWithTag("box");
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(all_objects.Length);
        if (capturado)
        {
            copyPosition();
        }
        if (!creado)
        {
       
            objetos_juntos();
        }

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
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = ultima_pos;
        /*cube.AddComponent<Pickupable>();

        EventTrigger eventTrigger1 = cube.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { capturar(); });
        eventTrigger1.triggers.Add(entry);
        */
        Debug.Log("OBJETO CREADO");
        creado = true;
    }
}
