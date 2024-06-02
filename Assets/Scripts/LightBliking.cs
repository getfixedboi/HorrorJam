using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Miniscript;
using System.IO;

public class LightBliking : MonoBehaviour
{
    private string _sourceCode;
    public Light lightComponent;
    public float blinkingSpeed;
    public float minIntensity;
    public float maxIntensity;
    public float skipTime;

    private Interpreter _interpreter = new Interpreter();

    void Start()
    {
        _sourceCode = $"while true \n Blink \n wait {skipTime.ToString().Replace(',', '.')} \n end while";
        lightComponent = GetComponent<Light>();

        Intrinsic f = Intrinsic.Create("Blink");
        f.code = (context, partialResult) =>
        {
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.Sin(Time.time * blinkingSpeed) * 0.5f + 0.5f);
            lightComponent.intensity = intensity;
            return new Intrinsic.Result(1);
        };

        _interpreter.errorOutput = (string s, bool l) => ErrorOutput(s, l);

        _interpreter.Reset(_sourceCode);
        _interpreter.Compile();
    }

    private void Update()
    {
        _interpreter.RunUntilDone(.2);
    }

    private void ErrorOutput(string s, bool l)
    {
        UnityEngine.Debug.LogError(s);
    }
}
