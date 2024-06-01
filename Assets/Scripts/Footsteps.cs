using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public PlayerMovement controller;
    private AudioSource source;
    public AudioClip[] clips;

    public float delay;
    private float footstepTimer;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (!controller.Character.isGrounded || controller.MoveInput == Vector3.zero)
        {
            return;
        }

        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0f && controller.MoveInput != Vector3.zero)
        {

            source.PlayOneShot(GetRandomClip());
            footstepTimer = delay;

        }
    }


    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
