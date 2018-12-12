using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallScript : MonoBehaviour {

    public float fallSpeed = 16.0f;
    public float spinSpeed = 250.0f;
    // Use this for initialization
    void Start () {
   
        Destroy(gameObject, 8);
        transform.localScale += new Vector3(3, 3, 3);
    }
	
	// Update is called once per frame
	void Update () {
        /*
        var pos = this.gameObject.transform.position;
        if (pos.y>1)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        }*/
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);

    }
}
