using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private GameObject _startTransition;
    [SerializeField] private GameObject _endTransition;
    private int _index;

    private void Start()
    {
        _index = SceneManager.GetActiveScene().buildIndex;
        if(_index == 0)
        {
            return;
        }
        _startTransition.SetActive(true);
        Invoke("DisableStart", 3f);
    }


    private void DisableStart()
    {
        _startTransition.SetActive(true);
    }

    public void ChangeScene()
    {

        StartCoroutine(C_Change());
    }

    private IEnumerator C_Change()
    {
        _endTransition.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_index + 1);

    }




    
}
