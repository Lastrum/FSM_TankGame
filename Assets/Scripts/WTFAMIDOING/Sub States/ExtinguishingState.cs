using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ExtinguishingState : RecoveringState
{
    public float healPercentage = 10;
    void Start()
    {
        healPercentage /= 100;
        
        transitions = new List<Transition>();
        transitions.Add(new Transition(() => Math.Abs(tank.health - tank.maxHealth) < 0.5f, gameObject.GetComponent<ReturningState>()));

    }
    
    void Update()
    {
        tank.health += (tank.maxHealth * healPercentage) * Time.deltaTime;
    }
}
