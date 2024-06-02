using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableGun : Interactable
{

    [SerializeField] private Text _popupText;
    [SerializeField] private Text _logoText;
    [SerializeField] private GameObject _gun;
    [SerializeField] private AudioSource _audioSource;
    public override void OnFocus()
    {
        _popupText.gameObject.SetActive(true);
        
    }

    public override void OnInteract()
    {
        _gun.SetActive(true);
        _audioSource.volume = 0.8f;
        _logoText.gameObject.SetActive(true);
        Destroy(gameObject);
      
    }

    public override void OnLoseFocus()
    {
        _popupText.gameObject.SetActive(false);
    }
}
