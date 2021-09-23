using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public List<Transition> transitions;
    public NPCTankController tank;
    public virtual void Awake()
    {
        transitions = new List<Transition>();
        tank = GetComponent<NPCTankController>();
        // TO-DO
        // setup your transitions here
    }
    
    public virtual void OnEnable()
    {
        // TO-DO
        // develop state's initialization here
    }
    
    public virtual void OnDisable()
    {
        // TO-DO
        // develop state's finalization here
    }
    
    public virtual void Update()
    {
        // TO-DO
        // develop behaviour here
    }
    
    public void LateUpdate()
    {
        foreach (Transition t in transitions)
        {
            if (t.condition.Test())
            {
                t.target.enabled = true;
                this.enabled = false;
                return;
            }
        }
    }
    
}
