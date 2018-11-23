using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {

    public bool capturado = false;
    // Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (capturado)
        {
            copyPosition();
        }

    }   
    
    public void copyPosition()
    {
        var pos = GameObject.Find("Head").transform.position;
        Debug.Log(pos);
        GameObject objeto;
        objeto = GameObject.Find("pCube");
        objeto.transform.position = new Vector3(pos[0],pos[1],pos[2]+3);        
    }
    public void capturar()
    {
        capturado = !capturado;
    }
}
