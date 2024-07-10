using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlinkText2 : MonoBehaviour
{
    [SerializeField] private Text _textHolder;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _delay;





    private void Start()
    {
        StartCoroutine(C_Blinking());
    }
    private IEnumerator C_Blinking()
    {
        
        _textHolder.gameObject.SetActive(true);


        while (true)
        {
            float startAlpha = _textHolder.color.a;
            float endAlpha = 0f;
            float timeElapsed = 0f;
            while (timeElapsed < _fadeDuration)
            {
                timeElapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / _fadeDuration);
                _textHolder.color = new Color(_textHolder.color.r, _textHolder.color.g, _textHolder.color.b, alpha);
                yield return null;
            }

            startAlpha = _textHolder.color.a;
            endAlpha = 1f;
            timeElapsed = 0f;
            while (timeElapsed < _fadeDuration)
            {
                timeElapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / _fadeDuration);
                _textHolder.color = new Color(_textHolder.color.r, _textHolder.color.g, _textHolder.color.b, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(_delay);
        }
    }

}
