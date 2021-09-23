using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class NPCTankController : MonoBehaviour
{

    public MonoBehaviour[] states;
    
    //GUI
    public TextMeshPro header;
    
    //Bullet settings
    public GameObject bullet;
    public float shootRate;
    public float elapsedTime;
    
    //public Object Bullet;
    public Transform turret { get; set; }
    public Transform bulletSpawnPoint { get; set; }
    
    //Tank Settings
    public int health = 100;
    public float patrolStateDistance = 300;
    public float chaseStateDistance = 200;
    public float attackStateDistance = 100;
    
    //List of points for patrolling
    public GameObject[] pointList;
    public Vector3 destPos;
    public float curRotSpeed;
    public float curSpeed;
    
    public Transform player;
    
    private string currentState;
    private void Start()
    {
        UpdateHeader();
        
        
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

        public void FindNextPoint()
        {
            //Debug.Log("Finding next point");
            int rndIndex = Random.Range(0, pointList.Length);
            Vector3 rndPosition = Vector3.zero;
            destPos = pointList[rndIndex].transform.position + rndPosition;
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

        private void Update()
        {
            //Check for health of bullet
            elapsedTime += Time.deltaTime;
            
            //Update Header
            UpdateHeader();
        }

        /// <summary>
        /// Shoot the bullet from the turret
        /// </summary>
        public void ShootBullet()
        {
            if (elapsedTime >= shootRate)
            {
                Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                elapsedTime = 0.0f;
            }
        }

        private void CheckState()
        {
            foreach (var s in states)
            {
                if (s == s.isActiveAndEnabled)
                {
                    Debug.Log(s.GetType().ToString());
                    currentState = s.GetType().ToString();
                }
            }
        }
        
        private void UpdateHeader()
        {
            CheckState();
            header.SetText( currentState + " | " + health);
        }
        
        
    
}

