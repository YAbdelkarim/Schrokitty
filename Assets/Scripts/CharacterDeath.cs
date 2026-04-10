using System;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    public static event Action OnAnyPlayerDie;
    public bool isDead = false;
    [SerializeField] private GameOverUI gameOverUI;
 
    private void OnEnable()
    {
        OnAnyPlayerDie += HandleDeath;
    }
    private void OnDisable()
    {
        OnAnyPlayerDie -= HandleDeath;
    }
    public void Die()
    {
        if (isDead) return;
        OnAnyPlayerDie?.Invoke();
    }

    private void HandleDeath()
    {
        isDead = true;
        Debug.Log(gameObject.name + " is dead");
        gameObject.SetActive(false);
        // GetComponent<Animator>().SetTrigger("Death");
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            gameOverUI.ShowGameOver();
            
            Die();
        }
    }
}