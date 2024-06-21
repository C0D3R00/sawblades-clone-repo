using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stomp : BehaviourAbstract
{
    [SerializeField]
    private Transform
        _overlapOrigin;

    //[SerializeField]
    //private LayerMask
    //    _overlapLayer;

    //[SerializeField]
    //private Vector2
    //    _overlapSize;

    [SerializeField]
    private float
        _jumpHeight = 2.5f;

    //protected override void Update()
    //{
    //    var hits = Physics2D.OverlapBoxAll(_overlapOrigin.position, _overlapSize, 0f, _overlapLayer).Where(h => h.CompareTag("Sawblade")).ToList();

    //    Debug.Log(hits.Count());

    //    foreach(var hit in hits)
    //    {
    //        hit.gameObject.SetActive(false);
    //    }

    //    if(hits != null && hits.Count > 0)
    //    {
    //        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpHeight);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sawblade"))
        {
            collision.gameObject.SetActive(false);

            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpHeight);
        }
    }
}
