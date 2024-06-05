using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private float _maxHealth;
    public static float CurrentHealth;

    public Text hptext;


    private void Start()
    {
        CurrentHealth = _maxHealth;
    }
    private void Update()
    {
        hptext.text = "Health: " + CurrentHealth.ToString();
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
