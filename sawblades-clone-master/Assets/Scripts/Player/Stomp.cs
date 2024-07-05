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
        _stompAirTime = .25f,
        _stompBounceForce = 2.5f;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.CompareTag("Enemy"))
        //{
        //    //collision.gameObject.SetActive(false);
        //    collision.GetComponent<Enemy>().OnStomped();

        //    _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpHeight);
        //}

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (IsStomping(collision))
            {                
                collision.collider.GetComponent<Enemy>().OnStomped();
                collision.gameObject.SetActive(false);

                _rb2d.velocity = new Vector2(_rb2d.velocity.x, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * _stompBounceForce));
                _playerState.IsStomping = true;

                StartCoroutine(StompDoneCo());
            }
            else
            {
                // Implement logic to damage the player here
                Debug.Log("Player hurt!");

                Time.timeScale = 0f;    
            }
        }
    }
    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        _playerState.IsStomping = true;
    //    }
    //}
    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        _playerState.IsStomping = false;
    //        Debug.Log("not stomping");
    //    }
    //}
    private bool IsStomping(Collision2D collision)
    {
        // Check if the player is above the enemy
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                return true;
            }
        }
        return false;
    }
    private IEnumerator StompDoneCo()
    {
        yield return new WaitForSeconds(_stompAirTime);

        _playerState.IsStomping = false;
    }
}
