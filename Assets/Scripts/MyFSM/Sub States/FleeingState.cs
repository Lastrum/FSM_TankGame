using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingState : RecoveringState
{
    public float fleeingSpeed = 200;
    public float fireDamagePercentage = 5;
    public GameObject waterPlane;

    private Vector3 dest;
    // Start is called before the first frame update
    void Start()
    {
        fireDamagePercentage /= 100;
        transitions = new List<Transition>();
        transitions.Add(new Transition(() => Vector3.Distance(transform.position, waterPlane.transform.position) <= 50,
            gameObject.GetComponent<ExtinguishingState>()));
    }

    public override void OnEnable()
    {
        tank.currentState = GetType().Name;
        recovery.position = transform.position;
        Debug.Log("LOOK: ??? "+recovery.position);
        Debug.Log("Fleeing");
        tank.curSpeed = fleeingSpeed;
        FindNextPoint();
    }

    public override void OnDisable()
    {
        recovery.fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(dest - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tank.curRotSpeed);

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * tank.curSpeed);

        tank.health -= (tank.maxHealth * fireDamagePercentage) * Time.deltaTime;
    }

    void FindNextPoint()
    {
        float value = 50;
        var position = new Vector3(Random.Range(-value, value), 0, Random.Range(-value, value));
        dest = waterPlane.transform.position + position;
    }
}
