using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int id;
    public float health;
    public float maxHealth = 100f;

    public void Initialise(int enemyId)
    {
        id = enemyId;
        health = maxHealth;
    }

    public void SetHealth(float enemyHealth)
    {
        health = enemyHealth;

        if(health <= 0f)
        {
            GameManager.enemies.Remove(id);
            Destroy(gameObject);
        }
    }
}
