using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Text deathTitleText;
    
    public Text deathTitleText1;

    public AudioSource source;

    public List<AudioClip> _clips;

    [TextArea] public string text;
    [TextArea] public string text1;

    bool canRevive = false;

    private void  OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        source = GetComponent<AudioSource>();
        StartCoroutine(C_Typing());
    }
    private IEnumerator C_Typing()
    {
        yield return new WaitForSeconds(1f);
        deathTitleText.color = Color.white;
        for (int i = 0; i < text.Length; i++)
        {
            if (i > 0 && text[i - 1] == '.')
            {
                deathTitleText.text += "";
                yield return new WaitForSeconds(.75f);
            }
            deathTitleText.text += text[i];
            source.PlayOneShot(GetRandomClip());
            yield return new WaitForSeconds(0.2f);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        yield return new WaitForSeconds(1f);


        ///////////////////////
        deathTitleText1.color = Color.white;
        for (int i = 0; i < text1.Length; i++)
        {
            if (i > 0 && text1[i - 1] == '?')
            {
                deathTitleText1.text += "";
                yield return new WaitForSeconds(.75f);
                canRevive=true;
            }
            deathTitleText1.text += text1[i];
            source.PlayOneShot(GetRandomClip());
            yield return new WaitForSeconds(0.2f);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        yield return new WaitForSeconds(1f);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& canRevive)
        {
            SceneManager.LoadScene(1);
        }
    }
    private AudioClip GetRandomClip()
    {
        return _clips[Random.Range(0, _clips.Count)];
    }
}

