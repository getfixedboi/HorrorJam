using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private float _maxHealth;
    public static float CurrentHealth;

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}
