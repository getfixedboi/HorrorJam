using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Scripts.Cam.Effects;
using Miniscript;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

[DisallowMultipleComponent]
public class GhostBehaviour : MonoBehaviour
{
    public enum GhostType
    {
        follower,
        suicide
    }
    public GhostType CurrentGhostType;

    private NavMeshAgent _agent;
    [SerializeField] private Material _origMaterial;
    private Material _material;
    private Animator _animator;
    private AudioSource _source;

    private Interpreter _interpreter;
    private string _sourceCode;

    private Transform _target;
    private bool _isScreaming = false;

    [SerializeField] List<AudioClip> _clips;

    public AudioClip punchSound;
    [SerializeField] private int _id;
    [Range(0.01f, 1f)][SerializeField] private float _timeToWait;

    private float _timer;
    public float FootstepDelay;
    private bool _isAttacking = false;
    public int CurrentHealth = 25;

    private bool _DisableMulInvoking = false;


    [HideInInspector]
    public bool IsCanTakeDamage;



    public ParticleSystem hit;
    public ParticleSystem hit1;


    private Rigidbody _rb;

    private void Awake()
    {
        IsCanTakeDamage = false;
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();

        _target = GameObject.FindWithTag("Player").transform;

        _material = new Material(_origMaterial);

        _interpreter = new Interpreter();
        _interpreter.errorOutput = (string s, bool l) => ErrorOutput(s, l);
        _sourceCode = _sourceCode = $"while true \n Dissapear{_id.ToString()} \n wait {_timeToWait.ToString().Replace(',', '.')} \n end while";

        Intrinsic f = Intrinsic.Create($"Dissapear{_id.ToString()}");
        f.code = (context, partialResult) =>
        {
            Disappearing(this.transform);
            return new Intrinsic.Result(1);
        };

        _interpreter.Reset(_sourceCode);
        _interpreter.Compile();
    }

    private void Update()
    {
        if (_DisableMulInvoking)
        {
            return;
        }
        if (_isScreaming)
        {
            _rb.isKinematic = true;
            GetComponent<BoxCollider>().enabled = false;
            _agent.enabled = false;
           // _agent.SetDestination(transform.position);
            _interpreter.RunUntilDone(.1);
            _animator.Play("death");
        }
        else if (Vector3.Distance(transform.position, _target.position) <= _agent.stoppingDistance && _isAttacking && CurrentGhostType == GhostType.follower && !_isScreaming)
        {
            _DisableMulInvoking = true;
            StartCoroutine(AttackCooldown());
        }
        else if (Vector3.Distance(transform.position, _target.position) <= _agent.stoppingDistance && !_isAttacking && CurrentGhostType == GhostType.follower && !_isScreaming)
        {
            _isAttacking = true;
        }
        else if(!_isScreaming)
        {
            GhostSteps();
            _agent.SetDestination(_target.position);
            _animator.Play("run");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") return;

        if (_isScreaming)
        {
            return;
        }

        if (CurrentGhostType == GhostType.suicide)
        {
            _source.Stop();
            _isScreaming = true;
            _source.PlayOneShot(_clips[0]);
            _target.GetComponent<PlayerHealth>().TakeDamage(15);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;

        if (_isScreaming)
        {
            return;
        }

        if (CurrentGhostType == GhostType.suicide)
        {
            _source.Stop();
            _isScreaming = true;
            _source.PlayOneShot(_clips[0]);
            _target.GetComponent<PlayerHealth>().TakeDamage(15);
        }
    }
   

    private void Disappearing(Transform parent)
    {
        float opacity = _material.color.a;
        Color color = _material.color;
        _material.color = new Color(color.r, color.g, color.b, opacity - 0.01f);

        ChangeMaterialsToLocal(parent, _material);

        if (opacity <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeMaterialsToLocal(Transform parent, Material material)
    {
        foreach (Transform child in parent)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }

            // Рекурсивно обрабатываем всех детей
            if (child.childCount > 0)
            {
                ChangeMaterialsToLocal(child, material);
            }
        }
    }

    private void GhostSteps()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _source.PlayOneShot(_clips[1]);
            _timer = FootstepDelay;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!IsCanTakeDamage)
        {
            return;
        }
        StartCoroutine(C_Hit());
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            _isScreaming = true;
            _DisableMulInvoking = false;
            _source.PlayOneShot(_clips[0]);
        }
    }

    private IEnumerator C_Hit()
    {
        hit1.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        hit1.gameObject.SetActive(false);

    }

    public void TakeScan()
    {
        Debug.Log("scan");
        StartCoroutine(C_Scaning());
    }
    private IEnumerator C_Scaning()
    {
        hit.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        hit.gameObject.SetActive(false);
        IsCanTakeDamage = true;

    }

    private IEnumerator AttackCooldown()
    {
        _agent.SetDestination(transform.position);
        _animator.Play("fix");
        _animator.Play("attack");
        _source.PlayOneShot(punchSound);
        yield return new WaitForSeconds(.90f);
        _target.GetComponent<PlayerHealth>().TakeDamage(15);
        yield return new WaitForSeconds(.85f);

        _isAttacking = false;
        _DisableMulInvoking = false;
    }

    private void ErrorOutput(string s, bool l)
    {
        UnityEngine.Debug.LogError(s);
    }
}
