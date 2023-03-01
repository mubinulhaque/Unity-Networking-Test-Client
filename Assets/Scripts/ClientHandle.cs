using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        string message = packet.ReadString();
        int myId = packet.ReadInt();

        Debug.Log($"Message from server: {message}");
        Client.instance.myId = myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet packet)
    {
        int id = packet.ReadInt();
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(id, username, position, rotation);
    }

    public static void PlayerPosition(Packet packet)
    {
        int playerId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        if (GameManager.players.TryGetValue(playerId, out PlayerManager player))
        {
            player.transform.position = position;
        }
    }

    public static void PlayerRotation(Packet packet)
    {
        int playerId = packet.ReadInt();
        Quaternion rotation = packet.ReadQuaternion();

        if (GameManager.players.TryGetValue(playerId, out PlayerManager player))
        {
            player.transform.rotation = rotation;
        }
    }

    public static void PlayerDisconnected(Packet packet)
    {
        int clientId = packet.ReadInt();
        if (GameManager.players.TryGetValue(clientId, out PlayerManager player))
        {
            Destroy(player.gameObject);

            GameManager.players.Remove(player.id);
        }
    }

    public static void PlayerHealth(Packet packet)
    {
        int id = packet.ReadInt();
        float health = packet.ReadFloat();

        GameManager.players[id].SetHealth(health);
    }

    public static void PlayerRespawned(Packet packet)
    {
        int id = packet.ReadInt();

        GameManager.players[id].Respawn();
    }

    public static void CreateItemSpawner(Packet packet)
    {
        int spawnerId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        bool hasItem = packet.ReadBool();

        GameManager.instance.CreateItemSpawner(spawnerId, position, hasItem);
    }

    public static void ItemSpawned(Packet packet)
    {
        int id = packet.ReadInt();
        GameManager.spawners[id].ItemSpawned();
    }

    public static void ItemPickedUp(Packet packet)
    {
        int playerId = packet.ReadInt();
        int spawnerId = packet.ReadInt();
        if (GameManager.spawners.TryGetValue(spawnerId, out ItemSpawner spawner))
        {
            spawner.ItemPickedUp();
        }
        GameManager.players[playerId].itemCount++;
    }

    public static void SpawnProjectile(Packet packet)
    {
        int projectileId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        int playerId = packet.ReadInt();
        GameManager.instance.SpawnProjectile(projectileId, position);
        GameManager.players[playerId].itemCount--;
    }

    public static void ProjectilePosition(Packet packet)
    {
        int projectileId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        if (GameManager.projectiles.TryGetValue(projectileId, out ProjectileManager projectile))
        {
            projectile.transform.position = position;
        }
    }

    public static void ProjectileExploded(Packet packet)
    {
        int projectileId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        GameManager.projectiles[projectileId].Explode(position);
    }

    public static void SpawnEnemy(Packet packet)
    {
        int enemyId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();

        GameManager.instance.SpawnEnemy(enemyId, position);
    }

    public static void EnemyPosition(Packet packet)
    {
        int enemyId = packet.ReadInt();
        Vector3 position = packet.ReadVector3();

        if (GameManager.enemies.TryGetValue(enemyId, out EnemyManager enemy))
        {
            enemy.transform.position = position;
        }
    }

    public static void EnemyHealth(Packet packet)
    {
        int enemyId = packet.ReadInt();
        float health = packet.ReadFloat();

        GameManager.enemies[enemyId].SetHealth(health);
    }
}
