using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scri : MonoBehaviour {
    public float spinValue=90;
    public TextMesh mapSelected;
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
        SceneManager.LoadScene("MundoMeteorito");
    }
    public void VerMeteorito()
    {
        //spinValue = -spinValue;
        mapSelected.text = "Mundo Meteorito";
    }

    public void EntrarTsunami()
    {
        //spinValue = -spinValue;
    }

    public void VerTsunami()
    {
        //spinValue = -spinValue;
        mapSelected.text = "Mundo Tsunami";
    }

    public void EntrarTerremoto()
    {
        //spinValue = -spinValue;
    }

    public void VerTerremoto()
    {
        //spinValue = -spinValue;
        mapSelected.text = "Mundo Terremoto";
    }

    public void LimpiarMessage()
    {
        //spinValue = -spinValue;
        mapSelected.text = "";
    }

}
