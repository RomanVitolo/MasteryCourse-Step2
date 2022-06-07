using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionMarkerUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _markerImage;
    [SerializeField] private Image _lockImage;
    
    public bool IsLockedIn { get; private set; }
    public bool IsPlayerIn { get { return _player.HasController; } }

    private CharacterSelectionMenuUI _menu;
    private bool initializing;
    private bool initialized;

    private void Awake()
    {
        _menu = GetComponentInParent<CharacterSelectionMenuUI>();
        _markerImage.gameObject.SetActive(false);
        _lockImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (IsPlayerIn == false) return; //We do nothing

        if (!initializing)  // If not initializing
        {
            StartCoroutine(Initialize());
        }

        if (!initialized) // If not initialized return nothing
        {
            return;   
        }

        if (!IsLockedIn)
        {
            //Check for player control and selection + locking character
            if (_player.Controller.horizontal > 0.5f)
            {
                MoveToCharacterPanel(_menu.RightPanelUI);
            }
            else if (_player.Controller.horizontal < 0.5f)
            {
                MoveToCharacterPanel(_menu.LeftPanelUI);
            }

            if (_player.Controller.AttackPressed)
            {
                StartCoroutine(LockCharacter());
            }
        }
        else
        {
            if (_player.Controller.AttackPressed)
            {
                StartCoroutine(WaitToStart());
                //_menu.TryStartGame();
            }
        }
        
    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(2);
        _menu.TryStartGame();
    }

    /*private void LockCharacter()
    {
        IsLockedIn = true;
        _lockImage.gameObject.SetActive(true);
        _markerImage.gameObject.SetActive(false);
    }*/
    private IEnumerator LockCharacter()
    {
        _lockImage.gameObject.SetActive(true);
        _markerImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        IsLockedIn = true;
    }

    private void MoveToCharacterPanel(CharacterSelectionPanelUI panel)
    {
        transform.position = panel.transform.position;
        _player.CharacterPrefab = panel.CharacterPrefab;
    }

    private IEnumerator Initialize()
    {
        initializing = true;
        MoveToCharacterPanel(_menu.LeftPanelUI);
        
        yield return new WaitForSeconds(0.5f);
        _markerImage.gameObject.SetActive(true);
        initialized = true;
    }
}
