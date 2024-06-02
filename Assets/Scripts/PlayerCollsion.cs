using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollsion : MonoBehaviour
{
    public static bool Second;
    public static bool Third;

    private void Awake()
    {
        Second = false;
        Third = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("2"))
        {
            Second = true;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("3"))
        {
            Third = true;
            Destroy(other.gameObject);
        }
    }
}
