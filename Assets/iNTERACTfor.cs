using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class iNTERACTfor : Interactable
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
        SceneManager.LoadScene(1);

    }

    public override void OnLoseFocus()
    {
        _popupText.gameObject.SetActive(false);
    }
}
