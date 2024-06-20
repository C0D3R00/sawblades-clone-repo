using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public abstract class BehaviourAbstract : MonoBehaviour
{
    [SerializeField]
    private List<MonoBehaviour>
        _disabledScripts;

    protected Rigidbody2D
        _rb2d;

    protected CollisionState
        _collisionState;

    protected PlayerState
        _playerState;

    protected virtual void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _collisionState = GetComponent<CollisionState>();
        _playerState = GetComponent<PlayerState>();
    }

    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void FixedUpdate() { }

    protected virtual void LateUpdate() { }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    protected void ToggleScripts(bool enable)
    {
        foreach (var item in _disabledScripts)
            item.enabled = enable;
    }
}
