using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;
using System.Collections.Generic;

public class Jump : BehaviourAbstract
{
    [SerializeField]
    private float
        _timeToJump = .5f,
        _jumpTopHeight = 7.5f;

    private float
        _jumpStartY = 0f,
        _jumpTimer = 0f;

    protected override void Update()
    {
        if (_playerState.IsJumping)
            if (_jumpTimer >= _timeToJump)
            {
                _playerState.IsJumping = false;
            }
            else
            {
                _jumpTimer += Time.deltaTime;
            }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (enabled)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    if (_collisionState.IsGrounded)
                    {
                        _playerState.IsJumping = true;
                        _jumpStartY = transform.position.y;
                        _jumpTimer = 0f;

                        _rb2d.velocity = new Vector2(_rb2d.velocity.x, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * _jumpTopHeight));
                    }

                    break;
                case InputActionPhase.Canceled:
                    if (_playerState.IsJumping &&
                        transform.position.y - _jumpStartY < _jumpTopHeight &&
                        _rb2d.velocity.y > 0f)
                    {
                        _playerState.IsJumping = false;
                        _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f);
                    }

                    break;
            }
        }
    }
}
