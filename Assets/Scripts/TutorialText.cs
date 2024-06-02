using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{

    private Text _tutorial;

    private void Awake()
    {
        _tutorial = GetComponent<Text>();
    }


    private void Update()
    {
        if (_tutorial.isActiveAndEnabled)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Destroy(gameObject);
            }
        }
    }

}
