using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class Pultik : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _pickUp;
    private bool _isPicking;
    private bool _isAttacking;
    [SerializeField] private float _delay;
   [SerializeField] private AudioClip _attack;
    [SerializeField] protected GameObject _camera;
    protected RaycastHit _hit;
    [SerializeField] protected float Distance;

    private void OnEnable()
    {
        StartCoroutine(C_PickUp());
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

  
    private IEnumerator C_PickUp()
    {
        _isPicking = true;
        _animator.Play("pultikpickup");
        _audioSource.PlayOneShot(_pickUp);
        yield return new WaitForSeconds(1.5f);
        _isPicking = false;
    }

 

    private void Update()
    {

        if (PauseMenu.IsPaused)
        {
            _audioSource.Stop();
            return;
        }

        if (_isPicking || _isAttacking)
        {
            return;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(C_Attacking());
        }
        else
        {
            _animator.Play("pultikIdle");
        }

    }

    private IEnumerator C_Attacking()
    {
        _isAttacking = true;
        _animator.Play("pultikAttack");
        _audioSource.PlayOneShot(_attack);
        DealDamage();
        yield return new WaitForSeconds(_delay);
        _isAttacking = false;

    }

    private void DealDamage()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, Distance))
        {
            GhostBehaviour enemy = _hit.collider.GetComponent<GhostBehaviour>();
            if (enemy != null)
            {
                enemy.TakeScan();
            }
        }
    }

}
