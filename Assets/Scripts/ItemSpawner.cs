using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public int id;
    public bool hasItem;
    public MeshRenderer model;
    public float rotationSpeed = 50f;
    public float bobSpeed = 2f;

    private Vector3 basePosition;

    private void Update()
    {
        if(hasItem)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
            transform.position = basePosition + new Vector3(0f, 0.25f * Mathf.Sin(Time.time * bobSpeed), 0f);
        }
    }

    public void Initialise(int spawnerId, bool withItem)
    {
        id = spawnerId;
        hasItem = withItem;
        model.enabled = withItem;

        basePosition = transform.position;
    }

    public void ItemSpawned()
    {
        hasItem = true;
        model.enabled = true;
    }

    public void ItemPickedUp()
    {
        hasItem = false;
        model.enabled = false;
    }
}