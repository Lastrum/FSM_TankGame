using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCTankController : MonoBehaviour
{
    public Transform playerTransform;
    
    //Bullet settings
    public GameObject bullet;
    public float shootRate;
    public float elapsedTime;
    
    //Tank Turret
    public Transform turret { get; set; }
    public Transform bulletSpawnPoint { get; set; }
    
    //Tank Settings
    public int health = 100;
    public float curRotSpeed;
    public float curSpeed;
    
    //List of points for patrolling
    public GameObject[] pointList;
    //Next destination position of the NPC Tank
    public Vector3 destPos;

    public Transform player;

    private void Start()
    {
        health = 100;

        elapsedTime = 0.0f;
        shootRate = 2.0f;

        //Get the target enemy(Player)
        player = GameObject.FindGameObjectWithTag("Player").transform;


        //Get the turret of the tank
        turret = gameObject.transform.GetChild(0).transform;
        bulletSpawnPoint = turret.GetChild(0).transform;
        
        //Get the list of points
        pointList = GameObject.FindGameObjectsWithTag("WandarPoint");

        Transform[] waypoints = new Transform[pointList.Length];
        int i = 0;
        foreach (GameObject obj in pointList)
        {
            waypoints[i] = obj.transform;
            i++;

        }
    }

    void OnCollisionEnter(Collision collision)
        {
            //Reduce health
            if (collision.gameObject.tag == "Bullet")
            {
                health -= 25;

                if (health <= 0)
                {
                    Debug.Log("Switch to Dead State");
                    Explode();
                }
            }
        }

        protected void Explode()
        {
            float rndX = Random.Range(10.0f, 30.0f);
            float rndZ = Random.Range(10.0f, 30.0f);
            for (int i = 0; i < 3; i++)
            {
                GetComponent<Rigidbody>().AddExplosionForce(10000.0f, transform.position - new Vector3(rndX, 10.0f, rndZ), 40.0f, 10.0f);
                GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(rndX, 20.0f, rndZ));
            }

            Destroy(gameObject, 1.5f);
        }

        /// <summary>
        /// Shoot the bullet from the turret
        /// </summary>
        public void ShootBullet()
        {
            if (elapsedTime >= shootRate)
            {
                //Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                elapsedTime = 0.0f;
            }
        }
    }

