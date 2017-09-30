using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRandTumble : MonoBehaviour
{

    public int Tumble;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * Tumble;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
