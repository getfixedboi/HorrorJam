using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Miniscript;
using System.IO;

public class TextTyping : MonoBehaviour
{
    private const string _textSource = "MiniscriptScripts\\introText.mns";
    private string _sourceText;
    [SerializeField] private Text _textHolder;
    [SerializeField] private AudioClip[] _clips;
    private AudioSource _audioSource;

    private void Awake()
    {
        Interpreter interpreter = new Interpreter();

        interpreter.Reset(File.ReadAllText(_textSource));
        interpreter.Compile();
        interpreter.RunUntilDone(.2);

        Value valueText = interpreter.GetGlobalValue("typingText");
        _sourceText = valueText.ToString();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(C_Typing());
    }
    private IEnumerator C_Typing()
    {
        yield return new WaitForSeconds(3f);
        _textHolder.color = Color.white;
        for (int i = 0; i < _sourceText.Length; i++)
        {
            if (i > 0 && _sourceText[i - 1] == '.')
            {
                _textHolder.text += "\n";
                yield return new WaitForSeconds(1f);
            }
            _textHolder.text += _sourceText[i];
            _audioSource.PlayOneShot(GetRandomClip());
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

    private AudioClip GetRandomClip()
    {
        return _clips[Random.Range(0, _clips.Length)];
    }


}
