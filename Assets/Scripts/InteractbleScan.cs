using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractbleScan : Interactable
{
    [SerializeField] private Text _popupText;
    [SerializeField] private Text _tutorialText;
    [SerializeField] private GameObject _scan;
    public override void OnFocus()
    {
        _popupText.gameObject.SetActive(true);

    }

    public override void OnInteract()
    {
        _scan.SetActive(true);
        _tutorialText.gameObject.SetActive(true);
        Destroy(gameObject);

    }

    public override void OnLoseFocus()
    {
        _popupText.gameObject.SetActive(false);
    }
}
