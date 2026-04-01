using UnityEngine;

public class PlayerWallHandler : MonoBehaviour
{
    // variables for wall detection
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private Transform _wallCheckRight, _wallCheckLeft;
    [SerializeField] private float _wallCheckRadius = 0.2f;
    [SerializeField] private float _wallHoldDuration = 1.5f;
    [SerializeField] private float _wallSlideSpeed = -2f;
    private float _wallHoldTimer;
    private bool _wallOnRight;
    private bool _wallOnLeft;
    private bool _isHoldingWall;

    private Rigidbody2D _rb;
    private float _defaultGravityScale;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _defaultGravityScale = _rb.gravityScale;
    }

    public void HandleWallInteractions(Vector2 _moveInput)
    {
        //wall logic
        if (IsWalled())
        {
            if (isMovingTowardsWall(_moveInput))
            {
                //hold wall then fall off after x seconds.
                if (!_isHoldingWall)
                {
                    //reset timer and zero out velocity
                    _isHoldingWall = true;
                    _wallHoldTimer = _wallHoldDuration;
                    _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
                }
                
                if(_wallHoldTimer > 0)
                {
                    // decrement timer and keep player on hold
                    _wallHoldTimer -= Time.fixedDeltaTime;
                    _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
                }
                else
                {
                    // on wall but timer ended
                    ReleaseWall(); 
                }
            }
            else
            {
                SlideWall();
            }
        }
        else
        {
            // not on wall or grounded, dont hold wall
            ReleaseWall();
        }
    }

    //wall checks
    private bool IsWalled()
    {
        _wallOnRight = Physics2D.OverlapCircle(_wallCheckRight.position, _wallCheckRadius, _wallLayer);
        _wallOnLeft = Physics2D.OverlapCircle(_wallCheckLeft.position, _wallCheckRadius, _wallLayer);
        return _wallOnRight || _wallOnLeft;
    }

    private bool isMovingTowardsWall(Vector2 _moveInput)
    {
        return (_wallOnRight && _moveInput.x > 0) || (_wallOnLeft  && _moveInput.x < 0);
    }

    private void ReleaseWall()
    {
        if (!_isHoldingWall) return; // already released
        _isHoldingWall = false;
        _rb.gravityScale = _defaultGravityScale; // restore your default gravity value
    }
    private void SlideWall()
    {
        _isHoldingWall = false;
        _rb.gravityScale = _defaultGravityScale;
        
        // Clamp downward velocity to a slow slide rather than a full fall
        if (_rb.linearVelocity.y < _wallSlideSpeed)
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _wallSlideSpeed);
    }
}
