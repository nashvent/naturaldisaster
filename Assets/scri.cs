using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scri : MonoBehaviour {
    public float spinValue=90;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up*spinValue*Time.deltaTime);
	} 

    public void flipSpin()
    {
        spinValue = -spinValue; 
    }

    public void EntrarMeteorito()
    {
        //spinValue = -spinValue;
    }

    public void EntrarTsunami()
    {
        //spinValue = -spinValue;
    }

    public void EntrarTerremoto()
    {
        //spinValue = -spinValue;
    }

}
