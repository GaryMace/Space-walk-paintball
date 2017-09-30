using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCollider : MonoBehaviour
{

    private ContactPoint collisionPoint;
    public bool HasCollided;
    public GameObject Bullet_Mark_Prefab;
    public Transform Shooter;
    private PlayerController Player_Controller;

    public int Score_Value;

    //Holds a Reference to the newly created Bullet Mark.
    private GameObject temporaryBulletMarkHandler;

    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("PlayerObject");
        if (playerControllerObject != null)
        {
            Player_Controller = playerControllerObject.GetComponent<PlayerController>();
        }
        if (playerControllerObject == null)
        {
            Debug.Log("Cant find game object");
        }
        HasCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision collided)
    {
        if ((collided.gameObject.name == "space_man_model" || collided.gameObject.name ==  "Ship" || collided.gameObject.name == "CameraObject") && !HasCollided)
        {
            collisionPoint = collided.contacts[0]; //Returns the FIRST Point of Contact.
            HasCollided = true;

            //Instantiate the Bullet Mark, the Magic Phrase that makes it all happen correctly here is: Quaternion.LookRotation(collisionPoint.normal)
            temporaryBulletMarkHandler =
                Instantiate(Bullet_Mark_Prefab, collisionPoint.point, Quaternion.LookRotation(collisionPoint.normal));
            temporaryBulletMarkHandler.transform.parent = collided.transform;
            //Compensate for the Rotation Error, this may or may not be required for your Bullet Mark Mesh depending
            //on how it was created.
            if (Shooter.tag == "Hero" && collided.gameObject.name == "space_man_model")
            {
                temporaryBulletMarkHandler.transform.Rotate(Vector3.right * 180);
                Player_Controller.AddHeroScore(Score_Value);
            }
            else if (Shooter.tag == "Baddie" && collided.gameObject.name == "CameraObject")
            {
                Player_Controller.AddEnemyScore(Score_Value);
            }
            //We have to "Push" the Bullet Texture out of the wall a bit [just a little] least it be hidden inside the walls of contact.
            temporaryBulletMarkHandler.transform.Translate(Vector3.up * 0.005f);

            //Destroy(Temporary_Bullet_Mark_Handler, 3.0f); //Destroy the Bullet Mark after 3 Seconds.
            Destroy(gameObject); //Destroy the Bullet itself.
        }
    }

    void OnCollisionExit(Collision collided)
    {
        if (collided.gameObject.name == "space_man_model")
        {
            temporaryBulletMarkHandler.transform.parent = null;
        }
    }
}