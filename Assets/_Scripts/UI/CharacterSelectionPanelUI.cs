using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionPanelUI : MonoBehaviour
{
    [SerializeField] private Character _characterPrefab;
    public Character CharacterPrefab { get { return _characterPrefab; } }
}
