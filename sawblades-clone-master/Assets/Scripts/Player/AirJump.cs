using UnityEngine;
using UnityEngine.InputSystem;

public class AirJump : BehaviourAbstract
{
    [SerializeField]
    private float
        _timeToDoubleJump = .5f,
        _doubleJumpTopHeight = 7.5f;

    private float
        _jumpStartY = 0f,
        _jumpTimer = 0f;

    private bool
        _canAirJump;

    protected override void Update()
    {
        if (_playerState.IsAirJumping)
            if (_jumpTimer >= _timeToDoubleJump)
            {
                _playerState.IsAirJumping = false;
            }
            else
            {
                _jumpTimer += Time.deltaTime;
            }

        if (_collisionState.IsGrounded ||
           _collisionState.IsOnWallLeft || 
           _collisionState.IsOnWallRight)
        {
            _playerState.IsAirJumping = false;
            _canAirJump = true;
        }
    }

    public void OnAirJump(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    Debug.Log(_canAirJump);
                    if (!_collisionState.IsGrounded && _canAirJump)
                    {
                        _canAirJump = false;
                        _playerState.IsAirJumping = true;
                        _jumpStartY = transform.position.y;
                        _jumpTimer = 0f;

                        _rb2d.velocity = new Vector2(_rb2d.velocity.x, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * _doubleJumpTopHeight));
                    }

                    break;
                case InputActionPhase.Canceled:
                    if (_playerState.IsAirJumping &&
                        transform.position.y - _jumpStartY < _doubleJumpTopHeight &&
                        _rb2d.velocity.y > 0f)
                    {
                        _playerState.IsAirJumping = false;
                        _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f);
                    }

                    break;
            }
        }
    }
}
