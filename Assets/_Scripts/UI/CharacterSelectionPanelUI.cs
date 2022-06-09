using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionPanelUI : MonoBehaviour
{
    //1 code Line
    [field: SerializeField] public Character CharacterPrefab { get; set; }  
    
    //Old Version
    //[SerializeField] private Character _characterPrefab;
    //public Character CharacterPrefab { get { return _characterPrefab; } }
}
