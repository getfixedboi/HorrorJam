using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miniscript;
using UnityEngine.UI;
using System.IO;
using static Miniscript.TAC;
using System.Diagnostics.SymbolStore;

public class DialogueSystem : MonoBehaviour
{
    private const string _filepath = "MiniscriptScripts\\dialogue.mns";
    private Interpreter _interpreter = new Interpreter();


    [SerializeField] private Text _sourceText;
    [SerializeField] private GameObject _dialogueWindow;

    private ValMap _config;


    public static bool IsDialogue;

    private AudioSource _audioSource;


    [SerializeField] private AudioClip[] _firstConversation;
    [SerializeField] private AudioClip[] _secondConversation;
    [SerializeField] private AudioClip[] _thirdConversation;


    public GameObject forgivines;
    public GameObject enemies;





    private void Awake()
    {
        _interpreter.Reset(File.ReadAllText(_filepath));
        _interpreter.Compile();
        _interpreter.RunUntilDone();
        _config = _interpreter.GetGlobalValue("lines") as ValMap;
        IsDialogue = false;
        _audioSource = GetComponent<AudioSource>();
        _interpreter.standardOutput = (string s, bool l) => MiniScriptOutput(s, l);
        _interpreter.implicitOutput = (string s, bool l) => MiniScriptOutput(s, l);
        _interpreter.errorOutput = (string s, bool l) => MiniScriptOutput(s, l);
    }

    private void Start()
    {
        Invoke("FirstDialogue", 5f);
    }

    private void FirstDialogue()
    {
        StartCoroutine(C_First());
    }

    private IEnumerator C_First()
    {
        IsDialogue = true;
        _dialogueWindow.SetActive(true);
        _sourceText.text = _config["1"].ToString();
        _audioSource.PlayOneShot(_firstConversation[0]);
        yield return new WaitForSeconds(_firstConversation[0].length);
        _sourceText.text = _config["2"].ToString();
        _audioSource.PlayOneShot(_firstConversation[1]);
        yield return new WaitForSeconds(_firstConversation[1].length);
        _dialogueWindow.SetActive(false);
        IsDialogue = false;
    }
    private IEnumerator C_Second()
    {
        IsDialogue = true;
        _dialogueWindow.SetActive(true);
        _sourceText.text = _config["3"].ToString();
        _audioSource.PlayOneShot(_secondConversation[0]);
        yield return new WaitForSeconds(_secondConversation[0].length);
        _sourceText.text = _config["4"].ToString();
        _audioSource.PlayOneShot(_secondConversation[1]);
        yield return new WaitForSeconds(_secondConversation[1].length);
        _dialogueWindow.SetActive(false);
        IsDialogue = false;
        PlayerCollsion.Second = false;
    }

    private IEnumerator C_Third()
    {
        IsDialogue = true;
        _dialogueWindow.SetActive(true);
        _sourceText.text = _config["5"].ToString();
        _audioSource.PlayOneShot(_thirdConversation[0]);
        yield return new WaitForSeconds(_thirdConversation[0].length);
        _sourceText.text = _config["6"].ToString();
        _audioSource.PlayOneShot(_thirdConversation[1]);
        yield return new WaitForSeconds(_thirdConversation[1].length);
        _sourceText.text = _config["7"].ToString();
        _audioSource.PlayOneShot(_thirdConversation[2]);
        yield return new WaitForSeconds(_thirdConversation[2].length);
        _sourceText.text = _config["8"].ToString();
        _audioSource.PlayOneShot(_thirdConversation[3]);
        yield return new WaitForSeconds(_thirdConversation[3].length);
        _sourceText.text = _config["9"].ToString();
        _audioSource.PlayOneShot(_thirdConversation[4]);
        yield return new WaitForSeconds(_thirdConversation[4].length);
        _sourceText.text = _config["10"].ToString();
        _audioSource.PlayOneShot(_thirdConversation[5]);
        yield return new WaitForSeconds(_thirdConversation[5].length);
        IsDialogue = false;
        _dialogueWindow.SetActive(false);
        PlayerCollsion.Third = false;
        enemies.SetActive(true);
        forgivines.SetActive(true);
    }


    private void Update()
    {
        if (PlayerCollsion.Second && !IsDialogue)
        {
            StartCoroutine(C_Second());
        }

        if (PlayerCollsion.Third && !IsDialogue)
        {
            StartCoroutine(C_Third());
        }



    }




    private void MiniScriptOutput(string s, bool lineOutbreak)
    {
        UnityEngine.Debug.Log(s);
    }
}
