using UnityEngine;
using UnityEngine.UI;
using Miniscript;
using System.IO;

[DisallowMultipleComponent]
public class PauseMenu : MonoBehaviour
{
    private const string _filepath = "MiniscriptScripts\\playerconfig.mns";
    private Interpreter _interpreter = new Interpreter();
    private ValMap _configMap;
    [Space]
    [SerializeField] private PlayerCameraMove _cameraMove;
    [Header("Canvas references")]
    [SerializeField] private Canvas _playerInterface;
    [SerializeField] private Canvas _pauseMenu;
    [Header("Settings elements references")]
    [SerializeField] private Slider _volumeSlider; // Добавили ссылку на слайдер громкости
    [SerializeField] private Slider _mouseSensitivitySlider; // Добавили ссылку на слайдер чувствительности мыши
    [SerializeField] private Toggle _vsyncToggle;

    private void Awake()
    {
        _pauseMenu.enabled = false;

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
        _cameraMove.MouseSensivity = (float)_configMap["mouseSensivity"].FloatValue();
        AudioListener.volume = _configMap["gloablVolume"].FloatValue();
        QualitySettings.vSyncCount = _configMap["vsync"].IntValue() == 0 ? 0 : 1;

        _vsyncToggle.isOn = QualitySettings.vSyncCount != 0;
        _volumeSlider.value = AudioListener.volume; // Устанавливаем значение слайдера громкости
        _mouseSensitivitySlider.value = _cameraMove.MouseSensivity; // Устанавливаем значение слайдера чувствительности мыши
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_pauseMenu.enabled)
            {
                OpenPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    private void OpenPauseMenu()
    {
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _playerInterface.enabled = false;
        _pauseMenu.enabled = true;
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _playerInterface.enabled = true;
        _pauseMenu.enabled = false;

        SaveConfigValues();
    }


    public void QuitGame()
    {
        ClosePauseMenu();
        Application.Quit(1);
    }

    private void SaveConfigValues()
    {
        _configMap["mouseSensivity"] = new ValNumber(_cameraMove.MouseSensivity);
        _configMap["gloablVolume"] = new ValNumber(AudioListener.volume);
        _configMap["vsync"] = new ValNumber(QualitySettings.vSyncCount);

        _interpreter.SetGlobalValue("config", _configMap);

        string script = $"config = {{\n" +
                    $"    \"mouseSensivity\": {_configMap["mouseSensivity"].FloatValue().ToString().Replace(',', '.')},\n" +
                    $"    \"gloablVolume\": {_configMap["gloablVolume"].FloatValue().ToString().Replace(',', '.')},\n" +
                    $"    \"vsync\": {_configMap["vsync"].IntValue()}" +
                    $"}}";

        File.WriteAllText(_filepath, script);
    }

    private void Output(string s, bool l)
    {
        UnityEngine.Debug.Log(s);
    }

    public void ToggleVSync()
    {
        QualitySettings.vSyncCount = _vsyncToggle.isOn ? 1 : 0;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = _volumeSlider.value;
    }

    public void ChangeMouseSensitivity()
    {
        _cameraMove.MouseSensivity = _mouseSensitivitySlider.value;
    }
}
