using UnityEngine;
using System;


public class TestSP : MonoBehaviour
{

    ObjectSuperposition objectSuperposition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        objectSuperposition = GetComponent<ObjectSuperposition>();
        objectSuperposition.stateActions = new Action<PlayerSuperposition>[objectSuperposition.noOfStates];
        objectSuperposition.stateActions[0] = state0;
        objectSuperposition.stateActions[1] = state1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void state0(PlayerSuperposition playerSuperposition)
    {
        Debug.Log("B");
    }

    void state1(PlayerSuperposition playerSuperposition)
    {
        playerSuperposition.transform.position = new Vector2(0,0);
    }
}
