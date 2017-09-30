using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieScript : MonoBehaviour {

    public Transform center;
    public float degreesPerSecond;
    public Transform enemy;

    public GameObject Bullet_Emitter;

    public GameObject Clear_Effect;

    public float Spray;

    public int PaintShotFrom;

    public int PaintBurstSize;

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;

    //Enter the Speed of the Bullet from the Component Inspector.
    public float Bullet_Forward_Force;

    private Vector3 v;
    private bool IsFiring;
    // Use this for initialization
    void Start () {
        v = transform.position - center.position;
	    AttemptToFire();
    }

    // Update is called once per frame
    void Update ()
	{
	    Move();
	    if (transform.childCount > 15)
	    {
	        ClearPaint();
	    }
	}

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.position, 0.9f * Time.deltaTime);
        transform.LookAt(enemy);
    }

    public void AttemptToFire()
    {
        StartCoroutine(PaintStream());
    }

    public IEnumerator PaintStream()
    {
        while (true)
        {
            for (int i = 0; i < PaintBurstSize; i++)
            {
                transform.GetComponent<AudioSource>().Play();
                SpawnPaint();
                yield return new WaitForSeconds(0.02f);
            }
            yield return new WaitForSeconds(Random.Range(3f, 10f));
        }
    }

    private void SpawnPaint()
    {
        Quaternion spray = Quaternion.Euler(
            Random.Range(-Spray, Spray),
            Random.Range(-Spray, Spray),
            Random.Range(-Spray, Spray)
        );

        GameObject paint = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation * spray);
        paint.GetComponent<PaintCollider>().Shooter = transform;    //So we can tell who shot it
        paint.transform.Rotate(Vector3.forward * 180);
        Rigidbody rb = paint.GetComponent<Rigidbody>();

        float parentSpeed = transform.GetComponent<Rigidbody>().velocity.magnitude;
        float bulletSpeed = Bullet_Forward_Force + parentSpeed;
        rb.velocity = (rb.transform.forward * bulletSpeed);

        Destroy(paint, 4.0f);
        IsFiring = false;
    }

    private void ClearPaint()
    {
        Transform child;
        StartCoroutine(CreateEffect());

        while ((child = transform.Find("Paintmark(Clone)")) != null)
        {
            child.transform.parent = null;
            DestroyImmediate(child.gameObject);
        }
    }

    private IEnumerator CreateEffect()
    {
        GameObject effect = Instantiate(
            Clear_Effect,
            transform.position,
            transform.rotation
        );

        yield return new WaitForSeconds(0.5f);
        Destroy(effect);
    }

    void OnCollisionEnter(Collision col)
    {
        
    }
}
