using System.Collections;
using System.Collections.Generic;
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
    private NavMeshAgent _agent;
    [SerializeField] private Material _origMaterial;
    private Material _material;
    private Animator _animator;
    private AudioSource _source;

    private Interpreter _interpreter;
    private string _sourceCode;

    private Transform _target;
    private bool _isScreaming = false;
    [Range(0.01f, 1f)][SerializeField] private float _timeToWait;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();

        _target = GameObject.FindWithTag("Player").transform;

        _material = new Material(_origMaterial);

        _interpreter = new Interpreter();
        _interpreter.errorOutput = (string s, bool l) => ErrorOutput(s, l);
        _sourceCode = _sourceCode = $"while true \n Dissapear \n wait {_timeToWait.ToString().Replace(',', '.')} \n end while";

        Intrinsic f = Intrinsic.Create("Dissapear");
        f.code = (context, partialResult) =>
        {
            Disappearing();
            return new Intrinsic.Result(1);
        };

        _interpreter.Reset(_sourceCode);
        _interpreter.Compile();
    }

    private void Update()
    {
        if (_isScreaming)
        {
            _interpreter.RunUntilDone(.1);
            _animator.Play("idle");
        }
        else
        {
            _agent.SetDestination(_target.position);
            _animator.Play("run");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _source.Play();
            _isScreaming = true;
        }
    }

    private void Disappearing()
    {
        Transform parent = transform;

        float opacity = _material.color.a;
        Color color = _material.color;
        _material.color = new Color(color.r, color.g, color.b, opacity-0.01f);

        ChangeMaterialsToLocal(parent,_material);

        if(opacity<=0)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeMaterialsToLocal(Transform parent,Material material)
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
                ChangeMaterialsToLocal(child,material);
            }
        }
    }

    private void ErrorOutput(string s, bool l)
    {
        UnityEngine.Debug.LogError(s);
    }
}
