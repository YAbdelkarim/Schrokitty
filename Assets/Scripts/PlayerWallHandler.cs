using UnityEngine;

public class PlayerWallHandler : MonoBehaviour
{
    // variables for wall detection
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private Transform _wallCheckRight, _wallCheckLeft;
    [SerializeField] private float _wallCheckRadius = 0.2f;
    [SerializeField] private float _wallHoldDuration = 1.5f;
    [SerializeField] private float _wallSlideSpeed = -2f;
    [SerializeField] private float _wallSlideGravity = 0.5f;
    private float _wallHoldTimer;
    private bool _wallOnRight;
    private bool _wallOnLeft;
    private bool _isHoldingWall;
    private bool _wallHoldExhausted = false;


    private Rigidbody2D _rb;
    private float _defaultGravityScale;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _defaultGravityScale = _rb.gravityScale;
        // Debug.Log($"[WallHandler] Initialized — defaultGravityScale: {_defaultGravityScale}");
    }

    public void HandleWallInteractions(Vector2 moveInput)
    {
        if (IsWalled())
        {
            if (isMovingTowardsWall(moveInput) && !_wallHoldExhausted)
            {
                if (!_isHoldingWall)
                {
                    _isHoldingWall = true;
                    _wallHoldTimer = _wallHoldDuration;
                    _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
                    // Debug.Log("[WallHandler] Grabbed wall — timer started");
                }
                
                if (_wallHoldTimer > 0)
                {
                    _wallHoldTimer -= Time.fixedDeltaTime;
                    _rb.gravityScale = 0f;
                    _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
                    // Debug.Log($"[WallHandler] Holding wall — timer: {_wallHoldTimer:F2}");
                }
                else
                {
                    _wallHoldExhausted = true; // prevent re-grabbing same wall
                    // Debug.Log($"[WallHandler] Hold timer expired — sliding | velocity.y: {_rb.linearVelocity.y:F2}");
                    SlideWall(); 
                }
            }
            else
            {
                // Debug.Log($"[WallHandler] On wall but not pressing into it — sliding | velocity.y: {_rb.linearVelocity.y:F2}");
                SlideWall();
            }
        }
        else
        {
            // if (_isHoldingWall)
            //     Debug.Log("[WallHandler] Left wall — releasing");

            _wallHoldExhausted = false; // reset exhaustion when leaving wall
            ReleaseWall();
        }
    }

    private bool IsWalled()
    {
        _wallOnRight = Physics2D.OverlapCircle(_wallCheckRight.position, _wallCheckRadius, _wallLayer);
        _wallOnLeft = Physics2D.OverlapCircle(_wallCheckLeft.position, _wallCheckRadius, _wallLayer);
        
        // Debug.Log($"[WallHandler] IsWalled check — right: {_wallOnRight}, left: {_wallOnLeft} | rightPos: {_wallCheckRight.position}, leftPos: {_wallCheckLeft.position}");
        
        return _wallOnRight || _wallOnLeft;
    }

    private bool isMovingTowardsWall(Vector2 moveInput)
    {
        bool pressing = (_wallOnRight && moveInput.x > 0) || (_wallOnLeft && moveInput.x < 0);
        // Debug.Log($"[WallHandler] isMovingTowardsWall: {pressing} | input.x: {moveInput.x}, wallRight: {_wallOnRight}, wallLeft: {_wallOnLeft}");
        return pressing;
    }

    private void ReleaseWall()
    {
        if (!_isHoldingWall) return;
        _isHoldingWall = false;
        _wallHoldTimer = 0f;
        _rb.gravityScale = _defaultGravityScale;
        // Debug.Log($"[WallHandler] ReleaseWall — gravity restored to {_defaultGravityScale}");
    }

    private void SlideWall()
    {
        _rb.gravityScale = _wallSlideGravity;
        if (_rb.linearVelocity.y < _wallSlideSpeed)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _wallSlideSpeed);
            // Debug.Log($"[WallHandler] SlideWall — clamping velocity to {_wallSlideSpeed}");
        }
    }

    // // gizmos for debugging wall checks
    //  private void OnDrawGizmos()
    // {
    //     if (_wallCheckRight != null && _wallCheckLeft != null)
    //     {
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawWireSphere(_wallCheckRight.position, _wallCheckRadius);
    //         Gizmos.DrawWireSphere(_wallCheckLeft.position, _wallCheckRadius);
    //     }
    // }
}