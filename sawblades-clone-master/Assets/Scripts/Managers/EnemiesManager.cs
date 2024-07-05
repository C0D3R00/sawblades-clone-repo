using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : Singleton<EnemiesManager>
{ 
    protected EnemiesManager    () { }

    [SerializeField]
    private Transform
        _left,
        _right;

    [SerializeField]
    private float
        _interval;

    [SerializeField]
    private int
        _minEnemy,
        _maxEnemy;

    private IEnumerator Start()
    {
        while (true)
        {
            var enemyCount = Random.Range(_minEnemy, _maxEnemy + 1);
            for (int i = 0; i < enemyCount; i++)
            {
                var enemy = ObjectPoolManager.Instance.GetPooledObject(ObjectPoolManager.ObjectPoolType.ENEMY);

                //sawblade.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                enemy.transform.position = new Vector2(Random.Range(_left.position.x, _right.position.x), _left.position.y);
                enemy.SetActive(true);
            }

            yield return new WaitForSeconds(_interval);
        }
    }
}
