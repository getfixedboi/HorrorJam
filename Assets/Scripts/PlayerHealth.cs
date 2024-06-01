using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
