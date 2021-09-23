using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    // Start is called before the first frame update
    void Start()
    {
        transitions = new List<Transition>();
        transitions.Add(new Transition(() => Vector3.Distance(transform.position, tank.player.position) >= tank.patrolStateDistance, gameObject.GetComponent<PatrolState>()));
        transitions.Add(new Transition(() => Vector3.Distance(transform.position, tank.player.position) <= tank.chaseStateDistance 
                                             && Vector3.Distance(transform.position, tank.player.position) >= tank.attackStateDistance, gameObject.GetComponent<ChaseState>()));
    }

    // Update is called once per frame
    void Update()
    {
        //Set the target position as the player position
        tank.destPos = tank.player.position;

        //Always Turn the turret towards the player
        Transform turret = transform.GetComponent<NPCTankController>().turret;
        Quaternion turretRotation = Quaternion.LookRotation(tank.destPos - turret.position);
        turret.rotation = Quaternion.Slerp(turret.rotation, turretRotation, Time.deltaTime * tank.curRotSpeed);
        
        //Shoot bullet towards the player
        transform.GetComponent<NPCTankController>().ShootBullet();
    }
}
