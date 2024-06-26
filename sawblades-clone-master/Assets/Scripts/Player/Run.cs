using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;
using System.Collections.Generic;

public class Run : BehaviourAbstract
{
    [SerializeField]
    private float
        _runSpeed = 25f,
        _runThreshold = .25f;

    private float
        _xAxis = 0f;

    protected override void FixedUpdate()
    {
        if ((_playerState.IsWallJumping && Mathf.Abs(_xAxis) > 0f) ||
            !_playerState.IsWallJumping)
            _rb2d.velocity = new Vector2(_xAxis * _runSpeed, _rb2d.velocity.y);        

        if (_xAxis != 0f)
            _playerState.IsRunning = true;
        else
            _playerState.IsRunning = false;

        if (!_playerState.IsWallSliding)
        {
            if (_xAxis > 0f)
                _playerState.IsFacingLeft = false;
            else if (_xAxis < 0f)
                _playerState.IsFacingLeft = true;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        var move = context.ReadValue<Vector2>();

        if (move.x > _runThreshold)
        {
            _xAxis = 1f;
        }
        else if (move.x < -_runThreshold)
        {
            _xAxis = -1f;
        }
        else
            _xAxis = 0f;
    }
}
