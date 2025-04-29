using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<BaseEnemy> enemies = new List<BaseEnemy>();

    void FixedUpdate()
    {
        foreach (var enemy in enemies)
        {
            enemy.CustomUpdate();
        }
    }

    public void RegisterEnemy(BaseEnemy enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
    }

    public void UnregisterEnemy(BaseEnemy enemy)
    {
        if (enemies.Contains(enemy))
            enemies.Remove(enemy);
    }
}
