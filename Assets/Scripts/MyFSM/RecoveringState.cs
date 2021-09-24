using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveringState : State
{
    public RecoveringState recovery;
    public GameObject fire;
    public Transform position;
    
    protected List<State> states;
    public State stateInitial;
    protected State stateCurrent;
    
    void Start()
    {
        fire.gameObject.SetActive(true);
        recovery = gameObject.GetComponent<RecoveringState>();
    }

    public override void OnEnable()
    {
        position = transform;

        if (stateCurrent == null)
            stateCurrent = stateInitial;
        stateCurrent.enabled = true;
    }
    
    public override void OnDisable()
    {
        base.OnDisable();
        stateCurrent.enabled = false;
        foreach (State s in states)
        {
            s.enabled = false;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
