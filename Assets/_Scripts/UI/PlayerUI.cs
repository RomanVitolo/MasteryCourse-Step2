using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private TextMeshProUGUI _tmpUI;

    private void Awake()
    {
        _tmpUI = GetComponent<TextMeshProUGUI>();
    }

    public void HandlePlayerInitialized()
    {
        _tmpUI.text = "PLayer Joined";
        StartCoroutine(ClearTextAfterDelay());
    }

    private IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(2);
        _tmpUI.text = String.Empty;
    }
}
