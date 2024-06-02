using System.Collections;
using System.Collections.Generic;
using Miniscript;
using UnityEngine;
using UnityEngine.Scripting;
using System.IO;

[DisallowMultipleComponent] 
public class PauseMenu : MonoBehaviour
{
    private const string _filepath = "MiniscriptScripts\\playerconfig.mns";
    private Interpreter _interpreter = new Interpreter();
    private ValMap _configMap;
    [Space]
    [SerializeField] private PlayerCameraMove _cameraMove;
    [SerializeField] private Canvas _pauseMenu;

    private void Awake()
    {
        LoadMiniscrpt();
        LoadConfigValues();
    }
    private void LoadMiniscrpt()
    {
        _interpreter.standardOutput = (string s, bool l) => Output(s, l);
        _interpreter.implicitOutput = (string s, bool l) => Output(s, l);
        _interpreter.errorOutput = (string s, bool l) => Output(s, l);

        _interpreter.Reset(File.ReadAllText(_filepath));
        _interpreter.Compile();
        _interpreter.RunUntilDone();

        _configMap = _interpreter.GetGlobalValue("config") as ValMap;
    }
    private void LoadConfigValues()
    {
        _cameraMove.MouseSensivity = _configMap["mouseSensivity"].FloatValue();
        AudioListener.volume = _configMap["gloablVolume"].FloatValue();
        QualitySettings.vSyncCount = _configMap["vsync"].IntValue() == 0 ? 0 : 1;
    }
    private void Output(string s, bool l)
    {
        UnityEngine.Debug.Log(s);
    }

}
