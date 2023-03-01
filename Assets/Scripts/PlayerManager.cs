using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float health;
    public float maxHealth = 100f;
    public int itemCount = 0;
    public MeshRenderer model;

    public void Initialise(int clientId, string name)
    {
        id = clientId;
        username = name;
        health = maxHealth;
    }

    public void SetHealth(float playerHealth)
    {
        health = playerHealth;

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        model.enabled = false;
    }

    public void Respawn()
    {
        model.enabled = true;
        SetHealth(maxHealth);
   }
}
