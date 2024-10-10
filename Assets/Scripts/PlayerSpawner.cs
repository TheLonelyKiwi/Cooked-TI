using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviour
{
    private List<Transform> _spawnPoints = new();
    private List<Player> _players = new ();
    
    void Start()
    {
        foreach (Transform child in transform)
        {
            _spawnPoints.Add(child);
        }

        _players = PlayerManager.instance.players;
        
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        for (int i =0; i < _players.Count; i++)
        {
            int randomIndex = Random.Range(0, _spawnPoints.Count);
            _players[i].gameObject.transform.position = _spawnPoints[randomIndex].position;
            _spawnPoints.Remove(_spawnPoints[randomIndex]);
        }
        Debug.Log("Succesfully spawned players!");
    }
}
