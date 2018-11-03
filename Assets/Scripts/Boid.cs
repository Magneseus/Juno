using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    List<Boid> neiboids;

	// Use this for initialization
	void Start ()
    {
        neiboids = new List<Boid>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.transform != this.transform)
        {
            Debug.Log(this);
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        Debug.Log("Exited");
    }
}
