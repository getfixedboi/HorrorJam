using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miniscript;
using System.IO;

public class Test : MonoBehaviour
{
    private const string _filepath = "MiniscriptScripts\\dialogue.mns";
    private Interpreter _interpreter = new Interpreter();

    private ValMap _config;

    private void Awake()
    {
        _interpreter.standardOutput = (string s, bool l) => MiniScriptOutput(s, l);
        _interpreter.implicitOutput = (string s, bool l) => MiniScriptOutput(s, l);
        _interpreter.errorOutput = (string s, bool l) => MiniScriptOutput(s, l);
    }

    private void Start()
    {
    }

    private void Update()
    {
        UnityEngine.Debug.Log(_config["1"].ToString());
    }

   

    private void MiniScriptOutput(string s, bool lineOutbreak)
    {
        UnityEngine.Debug.Log(s);
    }
}
