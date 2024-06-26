using UnityEngine;

public class WallSlide : BehaviourAbstract
{
    //[SerializeField]
    //private Transform
    //    _body;

    [SerializeField]
    private float
        _wallSlideVelocity = -0.5f/*,
        _spriteOffsetX = .4f*/;

    protected override void FixedUpdate()
    {
        if (_playerState.IsWallSliding && !_playerState.IsWallJumping)
        {
            if (!_collisionState.IsGrounded && 
                (_collisionState.IsOnWallLeft || _collisionState.IsOnWallRight))
            {
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, _wallSlideVelocity);
            }
            else
            {
                //_body.transform.localPosition = Vector2.zero;
                _playerState.IsWallSliding = false;

                ToggleScripts(true);
            }
        }
        else if (!_collisionState.IsGrounded &&
            !_playerState.IsJumping &&
            ((_playerState.IsFacingLeft && _collisionState.IsOnWallLeft) ||
            (!_playerState.IsFacingLeft && _collisionState.IsOnWallRight)))
        {
            //if (_playerState.IsFacingLeft)
            //{
            //    _spriteOffsetX = -Mathf.Abs(_spriteOffsetX);
            //}
            //else
            //{
            //    _spriteOffsetX = Mathf.Abs(_spriteOffsetX);
            //}

            //_body.transform.localPosition = new Vector3(_spriteOffsetX, 0f);
            _playerState.IsWallSliding = true;
            _playerState.IsFacingLeft = !_playerState.IsFacingLeft;

            ToggleScripts(false);
        }
    }
}
