using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Player[] _players;

    private void Awake()
    {
        _players = FindObjectsOfType<Player>();
    }

    public void AddPlayerToGame(Controller controller)
    {
        var firstActivePlayer = _players
            .OrderBy(t => t.PlayerNumber)
            .FirstOrDefault(t => t.HasController == false);

        //firstActivePlayer!.InitializeController(controller);
        if (firstActivePlayer != null)
        {
            firstActivePlayer.InitializeController(controller);  
        }
    }
}
