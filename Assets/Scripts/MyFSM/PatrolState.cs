using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PatrolState : State
{
    public float rotationSpeed = 1;
    public float speed = 100;

    public void Start()
    {
        transitions = new List<Transition>();
        transitions.Add(new Transition(() => Vector3.Distance(transform.position, tank.player.position) <= 200f, gameObject.GetComponent<ChaseState>()));
        transitions.Add(new Transition(() => tank.health <= tank.maxHealth*tank.recoveryPercentage, gameObject.GetComponent<RecoveringState>()));
    }
    
    public override void OnEnable()
    {
        tank.curSpeed = speed;
        tank.curRotSpeed = rotationSpeed;
    }
    
    public override void Update()
    {
        //Find another random patrol point if the current point is reached
		
        if (Vector3.Distance(transform.position, tank.destPos) <= 100.0f)
        {
            Debug.Log("Reached to the destination point\ncalculating the next point");
            tank.FindNextPoint();
        }

        //Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(tank.destPos - transform.position);
       transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tank.curRotSpeed);

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * tank.curSpeed);
    }
    
    
}
