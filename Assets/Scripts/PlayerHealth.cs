using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;
    public static float CurrentHealth;

    public Text heltsTEXT;

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    private void Update()
    {
        heltsTEXT.text = "Health: " + CurrentHealth;
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
