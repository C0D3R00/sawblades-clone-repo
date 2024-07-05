using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class AnimationState : BehaviourAbstract
{
    [SerializeField]
    private Animator
        _animator;

    protected override void Update()
    {
        // decide what animation to play based on player state
        _animator.SetInteger("VelocityX", _playerState.IsRunning ? 1 : 0);
        _animator.SetFloat("VelocityY", _rb2d.velocity.y);
        _animator.SetBool("IsGrounded", _collisionState.IsGrounded);
        _animator.SetBool("IsJumping", _playerState.IsJumping);
        _animator.SetBool("IsAirJumping", _playerState.IsAirJumping);
        _animator.SetBool("IsWallSliding", _playerState.IsWallSliding);
        _animator.SetBool("IsWallJumping", _playerState.IsWallJumping);
        _animator.SetBool("IsStomping", _playerState.IsStomping);
    }
}
