using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningState : RecoveringState
{
    
    void Start()
    {
        transitions = new List<Transition>();
        transitions.Add(new Transition(() => Vector3.Distance(transform.position, recovery.position) <= 20, gameObject.GetComponent<PatrolState>()));

    }

    public override void OnEnable()
    {
        tank.currentState = GetType().Name;
    }

    public override void OnDisable()
    {
        recovery.GetComponent<RecoveringState>().enabled = false;
    }

    void Update()
    {
        Debug.Log("LOOK FOR THIS: " + recovery.position);
        //Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(recovery.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tank.curRotSpeed);

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * tank.curSpeed);
        
    }
}
