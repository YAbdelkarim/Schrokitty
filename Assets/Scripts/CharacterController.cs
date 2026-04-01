using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f, _jumpForce = 5f;

    // variables for ground detection
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private PlayerWallHandler playerWallHandler;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveVector;
    private bool _jumpRequested = false;

    public static event Action OnAnyPlayerDie;
        
    private PlayerAnimator playerAnimator;

    private bool _isDead = false;

    [SerializeField] private GameObject _pauseMenu;
    private bool _paused = false;

    [SerializeField] private GameOverUI gameOverUI;

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void RestartScene()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Pause()
    {
        if (_isDead)
        {
            if (!_paused)
            {
                gameOverUI.HideGameOver();
                _pauseMenu.SetActive(true);
                _paused = true;
                Time.timeScale = 0f;
            }
            else
            {
                gameOverUI.ReshowGameOver();
                _pauseMenu.SetActive(false);
                _paused = false;
                Time.timeScale = 1f;
            }
        }
        else if (_paused) 
        { 
            _pauseMenu.SetActive(false);
            _paused = false;
            Time.timeScale = 1f;
        }
        else
        {
            _pauseMenu.SetActive(true);
            _paused = true;
            Time.timeScale = 0f;
        }

    }
    private void FixedUpdate()
    {
        _rigidbody.linearVelocityX = _moveVector.x * _movementSpeed;

        // Landing logic
        if(IsGrounded() && _rigidbody.linearVelocityY <= 0)
        {
            playerAnimator.handleLanding();
        }

        // Jump logic
        if(_jumpRequested && IsGrounded())
        {
            _rigidbody.linearVelocityY = _jumpForce;
            _jumpRequested = false;
            playerAnimator.handleJump();
        }
        else
        {
            _jumpRequested = false;
        }

        if(_rigidbody.linearVelocityY < 0)
        {
            playerAnimator.handleFall();
        }

        if (!IsGrounded)
        {
            playerWallHandler.HandleWallInteractions();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            gameOverUI.ShowGameOver();
            
            Die();
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
    }

    public void Move(Vector2 moveVector)
    {
        if (_isDead) {
            playerAnimator.handleMotion(new Vector2(0, 0));
            return; 
        }

        _moveVector = moveVector;
        playerAnimator.handleMotion(moveVector);
    }

    public void Jump()
    {
        if (_isDead) { return; }

        _jumpRequested = true;
    }

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
        if (_isDead) return;
        OnAnyPlayerDie?.Invoke();
    }

    private void HandleDeath()
    {
        _moveVector = Vector2.zero;
        _isDead = true;
        Debug.Log(gameObject.name + " is dead");
        // GetComponent<Animator>().SetTrigger("Death");
    }
}