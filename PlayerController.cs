using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Clear_Effect;
    public GUIText Hero_Score_Text;
    public GUIText Enemy_Score_Text;
    public int Max_Speed;

    private int Hero_Score;
    private int Enemy_Score;

    private float pitchThruster;
    private float rollThruster;
    private float yawThruster;
    private float accelerateThruster;

    void Start()
    {
        Hero_Score = 0;
        Enemy_Score = 0;
        UpdateScore();
    }

    private IEnumerator CreateEffect()
    {
        GameObject effect = Instantiate(
            Clear_Effect,
            transform.Find("Main Camera").position,
            transform.Find("Main Camera").rotation
        );

        yield return new WaitForSeconds(0.5f);
        Destroy(effect);
    }

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float accelerate = Input.GetAxis("Accelerate");
        float pitch = Input.GetAxis("Pitch");
        float roll = Input.GetAxis("Roll");
        float yaw = Input.GetAxis("Yaw");
        if (Input.GetKey(KeyCode.Return))
        {
            accelerateThruster = 0;
            pitchThruster = 0;
            rollThruster = 0;
            yawThruster = 0;

            rb.velocity = new Vector3();
            rb.angularVelocity = new Vector3();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            yawThruster += yaw * Time.deltaTime * 2;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            pitchThruster += pitch * Time.deltaTime * 2;
        }
        if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.K))
        {
            rollThruster += roll * Time.deltaTime * 2;
        }
        if (Input.GetKey(KeyCode.X))
        {
            accelerateThruster = accelerate * Time.deltaTime * 50;

            if (rb.velocity.magnitude > Max_Speed)
            {
                rb.velocity = rb.velocity.normalized * Max_Speed;
            }
            else
            {
                rb.AddForce(transform.forward * accelerateThruster);

            }
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            Transform child;
            StartCoroutine(CreateEffect());

            while ((child = transform.Find("Paintmark(Clone)")) != null)
            {
                child.transform.parent = null;
                DestroyImmediate(child.gameObject);
            }
        }
       
        transform.Rotate (
            pitch * 45 * Time.deltaTime + pitchThruster, 
            yaw * 45 * Time.deltaTime + yawThruster, 
            roll * 45 * Time.deltaTime + rollThruster 
        );
    }

    public void AddHeroScore(int score)
    {
        Hero_Score += score;
        UpdateScore();
    }

    public void AddEnemyScore(int score)
    {
        Enemy_Score += score;
        UpdateScore();
    }

    void UpdateScore()
    {
        Hero_Score_Text.text = "Score: " + Hero_Score;
        Enemy_Score_Text.text = "Enemy Score: " + Enemy_Score;
    }
}
