using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sawblade"))
        {
            collision.gameObject.SetActive(false);

            Debug.Log(collision.name);
        }
    }
}
