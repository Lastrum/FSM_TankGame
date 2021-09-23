using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveringState : State
{
    public GameObject fire;
    public Transform position;
    
    
    // Start is called before the first frame update
    void Start()
    {
        transitions = new List<Transition>();
    }

    public override void OnEnable()
    {
        fire.gameObject.SetActive(true);
        position = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
