using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    ObjectSuperposition objectSuperposition;
    public Collider2D blockingCollider;
    public DoorButton[] doorButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        objectSuperposition = GetComponent<ObjectSuperposition>();
        objectSuperposition.stateActions = new Action<PlayerSuperposition>[objectSuperposition.noOfStates];
        objectSuperposition.stateActions[0] = Block;
        objectSuperposition.stateActions[1] = PassThrough;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PassThrough(PlayerSuperposition playerSuperposition)
    {
        Physics2D.IgnoreCollision(
            playerSuperposition.gameObject.GetComponent<Collider2D>(),
            blockingCollider
        );
    }

    void Block(PlayerSuperposition playerSuperposition)
    {
        Physics2D.IgnoreCollision(
            playerSuperposition.gameObject.GetComponent<Collider2D>(),
            blockingCollider,
            false
        );
    }

    public void CheckButtons()
    {
        foreach(DoorButton button in doorButtons)
        {
            if (!button.pushed)
                return;
        }
        objectSuperposition.currentState = 1;
        GetComponent<Animator>().SetBool("isClosed", false);
        GetComponent<AudioSource>().Play();
    }
}
