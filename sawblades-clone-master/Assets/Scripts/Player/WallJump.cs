using UnityEngine;
using UnityEngine.InputSystem;

public class WallJump : BehaviourAbstract
{
    [SerializeField]
    private float
        _wallJumpDistance = 3.75f,
        //_wallFallOffDistance = 3.75f,
        //_wallJumpClimbDistance = 3.75f,
        _wallJumpHeight = 7.5f,
        _timeToWallJump = .25f;

    private float
        _xAxis,
        _wallJumpTimer = 0f;

    protected override void FixedUpdate()
    {
        if (_playerState.IsWallJumping)
        {
            if(_wallJumpTimer < _timeToWallJump)
            {
                _wallJumpTimer += Time.deltaTime;
            }
            else
            {
                ToggleScripts(true);

                _playerState.IsWallJumping = false;
            }
        }
    }

    public void OnWallJump(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    //_rb2d.velocity = new Vector2((_playerState.IsFacingLeft ? -1 : 1) * _jumpDistance / _timeToJump, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * _jumpHeight));
                    if(_playerState.IsWallSliding)
                    {   
                        ToggleScripts(false);

                        _playerState.IsWallSliding = false;
                        _playerState.IsWallJumping = true;
                        _rb2d.velocity = new Vector2((_playerState.IsFacingLeft ? -1 : 1) * _wallJumpDistance, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * _wallJumpHeight));
                        _wallJumpTimer = 0f;
                    }

                    break;
                case InputActionPhase.Canceled:
                    if (_playerState.IsWallJumping)
                    {
                        ToggleScripts(true);

                        _playerState.IsWallJumping = false;
                        _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f);
                    }

                    break;
            }
        }
    }
}
