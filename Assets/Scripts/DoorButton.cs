using UnityEngine;

public class DoorButton : MonoBehaviour
{

    public Door door;
    public bool pushed;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!pushed)
            {
                pushed = true;
                audioSource.Play();
            }
            door.CheckButtons();
        }
        animator.SetBool("isPressed", pushed);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pushed)
            {
                pushed = false;
                audioSource.Play();
            }
        }
        animator.SetBool("isPressed", pushed);
    }
}

