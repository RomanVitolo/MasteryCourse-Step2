using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSelectionMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startGameText;
    
    [SerializeField] private CharacterSelectionPanelUI leftPanelUI;
    public CharacterSelectionPanelUI LeftPanelUI{ get { return leftPanelUI; } }

    [SerializeField] private CharacterSelectionPanelUI rightPanelUI;
    public CharacterSelectionPanelUI RightPanelUI{ get { return rightPanelUI; } }

    private CharacterSelectionMarkerUI[] _markerts;
    private int playerCount = 0;
    private int lockedCount = 0;

    private void Awake()
    {
        _markerts = GetComponentsInChildren<CharacterSelectionMarkerUI>();
        startGameText.gameObject.SetActive(false);
    }

    private void Update()
    {
        foreach (var marker in _markerts)
        {
            if (marker.IsPlayerIn)
            {
                playerCount++;
            }

            if (marker.IsLockedIn)
            {
                lockedCount++;
            }
        }
        CanStartGame();
        //bool startEnabled = playerCount > 0 && playerCount == lockedCount;
        //startGameText.gameObject.SetActive(startEnabled);
    }

    private void CanStartGame()
    {
        if (playerCount > 0 && lockedCount > 0)
        {
            startGameText.gameObject.SetActive(true);
        }
    }
}
