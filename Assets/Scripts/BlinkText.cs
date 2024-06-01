using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class BlinkText : MonoBehaviour
{
    [SerializeField] private Text _textHolder;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _delay;
    [SerializeField] private AudioClip _error;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        StartCoroutine(C_Blinking());
    }
    private IEnumerator C_Blinking()
    {
        yield return new  WaitForSeconds(21f);
        _textHolder.gameObject.SetActive(true);
        _audioSource.PlayOneShot(_error);


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