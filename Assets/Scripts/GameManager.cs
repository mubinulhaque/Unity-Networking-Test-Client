using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera mapCamera;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ItemSpawner> spawners = new Dictionary<int, ItemSpawner>();
    public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();
    public static Dictionary<int, EnemyManager> enemies = new Dictionary<int, EnemyManager>();
    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject itemSpawnerPrefab;
    public GameObject projectilePrefab;
    public GameObject enemyPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists! Destroying object...");
            Destroy(this);
        }
    }

    public PlayerManager GetPlayer(int id)
    {
        if (players.TryGetValue(id, out PlayerManager player)) {
            return players[id];
        } else
        {
            return null;
        }
    }

    public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation)
    {
        GameObject player;

        if(id == Client.instance.myId)
        {
            player = Instantiate(localPlayerPrefab, position, rotation);
            UIManager.instance.playerCamera = player.GetComponent<PlayerController>().playerCamera;
        } else
        {
            player = Instantiate(playerPrefab, position, rotation);
            UIManager.instance.addPlayer(id, username);
        }

        player.GetComponent<PlayerManager>().Initialise(id, username);

        players.Add(id, player.GetComponent<PlayerManager>());

        mapCamera.GetComponent<AudioListener>().enabled = false;
    }

    public void CreateItemSpawner(int spawnerId, Vector3 position, bool hasItem)
    {
        GameObject spawner = Instantiate(itemSpawnerPrefab, position, itemSpawnerPrefab.transform.rotation);
        spawner.GetComponent<ItemSpawner>().Initialise(spawnerId, hasItem);
        spawners.Add(spawnerId, spawner.GetComponent<ItemSpawner>());
    }

    public void SpawnProjectile(int id, Vector3 position)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
        projectile.GetComponent<ProjectileManager>().Initialise(id);
        projectiles.Add(id, projectile.GetComponent<ProjectileManager>());
    }

    public void SpawnEnemy(int id, Vector3 position)
    {
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemy.GetComponent<EnemyManager>().Initialise(id);
        enemies.Add(id, enemy.GetComponent<EnemyManager>());
    }
}
