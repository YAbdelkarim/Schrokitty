using System;
using UnityEngine;

public class ObjectSuperposition : MonoBehaviour
{

    public int noOfStates;
    public Action<PlayerSuperposition>[] stateActions;
    public int currentState = 0;

    private MasterSuperposition masterSuperposition;
    private ParticleSystem darkCloud;

    void Awake()
    {
        masterSuperposition = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterSuperposition>();
        darkCloud = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (masterSuperposition.superpositionMode)
        {
            if(!darkCloud.isPlaying)
                darkCloud.Play();
        }
        else
        {
            if (darkCloud.isPlaying)
                darkCloud.Stop();
        }
    }

    public void TriggerSuperposition(PlayerSuperposition player)
    {
        stateActions[player.SuperPositionWithRespectTo(gameObject)].Invoke(player);
    }

    public void TriggerCurrentState(PlayerSuperposition player)
    {
        stateActions[currentState].Invoke(player);
    }
}
