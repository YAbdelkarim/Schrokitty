using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBehavior : MonoBehaviour
{

    public string nextLevel;
    private bool isAtExit;
    private bool isOpen;
    private MasterSuperposition masterSuperposition;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        masterSuperposition = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterSuperposition>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isAtExit)
        {
            if (GameObject.FindGameObjectsWithTag("Key").Length == 0)
                SceneManager.LoadScene(nextLevel);
        }

        if (GameObject.FindGameObjectsWithTag("Key").Length == 0 && !isOpen)
        {
            GetComponent<Animator>().SetBool("allKeysCollected", true);
            audioSource.time = 1f;
            audioSource.Play();
            isOpen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !masterSuperposition.superpositionMode)
        {
            isAtExit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !masterSuperposition.superpositionMode)
        {
            isAtExit = false;
        }
    }
}
