using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miniscript;
using System.IO;

public class Test : MonoBehaviour
{
    private const string _filepath = "MiniscriptScripts\\_test.txt";

    private string extraCode="";
    Interpreter interpreter;

    private void Awake()
    {
        interpreter = new Interpreter();

        interpreter.standardOutput = (string s, bool l) => MiniScriptOutput(s, l);
        interpreter.implicitOutput = (string s, bool l) => MiniScriptOutput(s, l);
        interpreter.errorOutput = (string s, bool l) => MiniScriptOutput(s, l);

        CreateFunc();
    }

    private void Start()
    {
        interpreter.Reset(LoadScriptFromFile(_filepath)+extraCode);
        interpreter.Compile();
        interpreter.RunUntilDone();
    }

    private static void CreateFunc()
    {
        Intrinsic f = Intrinsic.Create("Name");
        f.code = (context, partialResult) =>
        {
            //body
            //
            return new Intrinsic.Result(0);//value to return
        };
    }

    private void MiniScriptOutput(string s, bool lineOutbreak)
    {
        UnityEngine.Debug.Log(s);
    }
    private string LoadScriptFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath); // Считывание всех строк файла как одну строку
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
            return string.Empty; // Возвращаем пустую строку в случае ошибки
        }

    }

}
