using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    
    private Player[] _players;

    private void Awake()
    {
        if (Instance !=null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        _players = FindObjectsOfType<Player>();
    }

    public void AddPlayerToGame(Controller controller)
    {
        var firstActivePlayer = _players
            .OrderBy(t => t.PlayerNumber)
            .FirstOrDefault(t => t.HasController == false);

        if (firstActivePlayer != null)
        {
            firstActivePlayer.InitializeController(controller);  
        }
    }

    public void SpawnPlayerCharacters()
    {
        foreach (var player in _players)
        {
            if (player.HasController && player.CharacterPrefab != null)
            {
                player.SpawnCharacter();
            }
        }
    }
}
