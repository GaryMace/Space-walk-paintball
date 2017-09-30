using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMoveMenu : MonoBehaviour
{

    public float Tumble;
    public int Velocity;
    public int Dir;

    private const int Right = 0;
    private const int Down = 1;

	// Use this for initialization
	void Start ()
	{
	    Rigidbody rb = GetComponent<Rigidbody>();

        rb.angularVelocity = Random.insideUnitSphere * Tumble;
        if (Dir == Right)
	        rb.velocity = Vector3.right * Velocity;
        else if (Dir == Down)
            rb.velocity = Vector3.up * -Velocity;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
