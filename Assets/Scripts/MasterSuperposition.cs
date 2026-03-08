using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MasterSuperposition : MonoBehaviour
{
    [Header("Settings")]
    public bool superpositionMode = false;
    public GameObject darkness;

    [Header("References")]
    public ArrayList clones;
    private Animator playerAnimator; // Cached reference to the animator

    public GameObject[] gameObjectsToDisable;

    private void Awake()
    {
        clones = new ArrayList();

        // Cache the animator on the player at the start
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerAnimator = playerObj.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleSuperpositionMode();
        }
    }

    public void ToggleSuperpositionMode()
    {
        // 1. Flip the mode
        superpositionMode = !superpositionMode;

        foreach (GameObject _object in gameObjectsToDisable)
        {
            _object.SetActive(!_object.activeSelf); 
        }

        // 2. Toggle the visual darkness overlay
        if (darkness != null)
        {
            darkness.SetActive(superpositionMode);
        }

        // 3. Update the Animator (This swaps your sprites/animations)
        if (playerAnimator != null)
        {
            // This matches the "isSuperpo" parameter in your Animator window
            playerAnimator.SetBool("isSuperposition", superpositionMode);
        }

        // 4. Handle Collapse or Interaction logic
        if (!superpositionMode)
        {
            Collapse();
        }

        // Use FindGameObjectWithTag (singular) for better performance and safety
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            PlayerSuperposition playerScript = playerObj.GetComponent<PlayerSuperposition>();

            if (playerScript != null && playerScript.currentCollision != null)
            {
                playerScript.InteractWith(playerScript.currentCollision);
            }
        }
    }

    void Collapse()
    {
        // Destroy all clones
        for (int i = clones.Count - 1; i >= 0; i--)
        {
            GameObject clone = (GameObject)clones[i];
            clones.RemoveAt(i);
            Destroy(clone);
        }

        // Reset the original player's state
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            PlayerSuperposition originalPlayer = playerObj.GetComponent<PlayerSuperposition>();
            if (originalPlayer != null)
            {
                originalPlayer.ResetPlayerStates();
            }
        }
    }
}