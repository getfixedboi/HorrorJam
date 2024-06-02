using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollsion : MonoBehaviour
{
    public static bool Second;

    private void Awake()
    {
        Second = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("2"))
        {
            Second = true;
            Destroy(other.gameObject);
        }
    }
}
