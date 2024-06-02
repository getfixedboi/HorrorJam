using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TransitionManager t;

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
            t.ChangeScene();
        }
    }


}
