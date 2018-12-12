using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickNew : MonoBehaviour {

    public bool capturado = false;
    // Use this for initialization
    void Start()
    {
        EventTrigger eventTrigger1 = this.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { capturar(); });
        eventTrigger1.triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {
        if (capturado)
        {
            copyPosition();
        }
    }

    public void copyPosition()
    {
        var pos = GameObject.Find("Head").transform.position;
        //objeto = GameObject.Find(this.gameObject.name);
        this.gameObject.transform.position = new Vector3(pos[0], pos[1], pos[2] + 3);
    }
    public void capturar()
    {
        capturado = !capturado;
        if (!capturado)
        {
            this.gameObject.transform.position = new Vector3(
                this.gameObject.transform.position.x,
                this.gameObject.transform.position.y - 1,
                this.gameObject.transform.position.z
                );
        }
    }
}
