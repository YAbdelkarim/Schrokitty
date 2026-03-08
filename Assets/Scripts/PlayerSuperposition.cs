using UnityEngine;
using System.Collections.Generic;

public class PlayerSuperposition : MonoBehaviour
{

    private Dictionary<GameObject, int> playerStates;
    private MasterSuperposition masterSuperposition;
    public Collider2D currentCollision;

    private Animator _anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerStates = new Dictionary<GameObject, int>();
        masterSuperposition = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterSuperposition>();

        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        InteractWith(other);
        currentCollision = other;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentCollision = null;
    }

    public void InteractWith(Collider2D other)
    {
        if (masterSuperposition.superpositionMode)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Superposition"))
            {
                if (SuperPositionWithRespectTo(other.gameObject) == -1)
                {
                    TriggerSuperpositionOn(other.gameObject);
                }
            }
        }
        else
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Superposition"))
                InteractWithoutSuperposition(other.gameObject);
        }
    }

    void TriggerSuperpositionOn(GameObject o)
    {
        ObjectSuperposition objectSuperposition = o.GetComponent<ObjectSuperposition>();
        int noOfStates = objectSuperposition.noOfStates;
        AddSuperpositionState(o, 0);
        objectSuperposition.TriggerSuperposition(this);
        GetComponent<AudioSource>().Play();
        for (int i = 1; i < noOfStates; i++)
        {
            GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
            Animator cloneAnim = clone.GetComponent<Animator>();
            if (cloneAnim != null)
            {
                // Force the clone into the superposition animation state immediately
                cloneAnim.SetBool("isSuperposition", true);
            }
            PlayerSuperposition cloneSuperposition = clone.GetComponent<PlayerSuperposition>();
            cloneSuperposition.AddSuperpositionState(o, i);
            objectSuperposition.TriggerSuperposition(cloneSuperposition);
            masterSuperposition.clones.Add(clone);
        }
    }

    void InteractWithoutSuperposition(GameObject o)
    {
        ObjectSuperposition objectSuperposition = o.GetComponent<ObjectSuperposition>();
        objectSuperposition.TriggerCurrentState(this);
    }

    public int SuperPositionWithRespectTo(GameObject o)
    {
        if (playerStates.TryGetValue(o, out int state))
        {
            return state;
        }
        return -1;
    }

    public void AddSuperpositionState(GameObject o, int state)
    {
        playerStates.Add(o, state);
    }

    public void ResetPlayerStates()
    {
        playerStates.Clear();
    }
}
