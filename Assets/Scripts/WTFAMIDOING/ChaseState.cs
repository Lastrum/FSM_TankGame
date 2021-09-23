using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    // Start is called before the first frame update
    void Start()
    {
        transitions = new List<Transition>();
        transitions.Add(new Transition(() => Vector3.Distance(transform.position, tank.player.position) >= tank.patrolStateDistance, gameObject.GetComponent<PatrolState>()));
        transitions.Add(new Transition(() => Vector3.Distance(transform.position, tank.player.position) <= tank.attackStateDistance, gameObject.GetComponent<AttackState>()));
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate to the target point
        tank.destPos = tank.player.position;

        Quaternion targetRotation = Quaternion.LookRotation(tank.destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tank.curRotSpeed);

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * tank.curSpeed);
    }
}
