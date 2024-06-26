using UnityEngine;

public class FaceDirection : BehaviourAbstract
{
    [SerializeField]
    private Transform
        _body;

    protected override void Start()
    {
        _playerState.IsFacingLeft = true;
    }
    protected override void Update()
    {
        if (_playerState.IsFacingLeft)
            _body.transform.localScale = new Vector2(1f, 1f);
        else
            _body.transform.localScale = new Vector2(-1f, 1f);
    }
}
