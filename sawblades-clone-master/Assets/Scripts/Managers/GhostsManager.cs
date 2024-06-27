using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostsManager : Singleton<GhostsManager>
{ 
    protected GhostsManager() { }

    [SerializeField]
    private Transform
        _left,
        _right;

    [SerializeField]
    private float
        _minSpawnInterval,
        _maxSpawnInterval;

    private IEnumerator Start()
    {
        while (true)
        {
            var sawblade = ObjectPoolManager.Instance.GetPooledObject(ObjectPoolManager.ObjectPoolType.GHOST);

            //sawblade.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            sawblade.transform.position = new Vector2(Random.Range(_left.position.x, _right.position.x), _left.position.y);
            sawblade.SetActive(true);

            yield return new WaitForSeconds(Random.Range(_minSpawnInterval, _maxSpawnInterval));
        }
    }
}
