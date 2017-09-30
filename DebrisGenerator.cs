using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisGenerator : MonoBehaviour
{
    public GameObject[] Asteroids;

    public float Tumble;

    public int Number_Asteroids;

    public int Asteriod_Belt_Circumference;

    public int Max_Asteroid_Scale;

    // Use this for initialization
    void Start ()
	{
	    for (int i = 0; i < Number_Asteroids; i++)
	    {
            int asteroidId = Random.Range(0, Asteroids.Length);
            GameObject asteroid = Instantiate(Asteroids[asteroidId], Random.insideUnitSphere * Asteriod_Belt_Circumference, transform.rotation);

            asteroid.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * Tumble;
	        asteroid.GetComponent<Rigidbody>().velocity = new Vector3(
                Random.Range(0, Tumble),
                Random.Range(0, Tumble),
                Random.Range(0, Tumble)
            );

            asteroid.GetComponent<Rigidbody>().transform.localScale = new Vector3(
                Random.Range(1, Max_Asteroid_Scale), 
                Random.Range(1, Max_Asteroid_Scale), 
                Random.Range(1, Max_Asteroid_Scale)
            );
	    }
    }

    // Update is called once per frame
    void Update () {
    }
}
