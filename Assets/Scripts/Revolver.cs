using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Revolver : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject _camera;
    [SerializeField] protected PlayerCameraMove cameraShake;
    [Header("For shake effect")]
    [SerializeField] protected float shakeDuration;
    [SerializeField] protected float magntitude;
    [Header("Gun stats")]
    [SerializeField] protected int Damage;
    [SerializeField] protected float Distance;
    [SerializeField] protected int MagazineSize;
    [SerializeField] protected float reloadTime;
    [Header("Lists")]
    [SerializeField] protected List<AudioClip> AudioClips;
    [SerializeField] protected List<float> shootDelays;
    protected Animator _animator;
    protected AudioSource _source;
    protected bool _isShoot;
    protected bool _isReload;
    protected int _currMagazineSize;
    protected RaycastHit _hit;


    private bool _isPicking;


    [SerializeField] private Text _logoText;


    public GameObject enemies;


    public GameObject playerStats;


    public Text bulelts;


    private void OnEnable()
    {
        StartCoroutine(C_PickUp());
    }

    private IEnumerator C_PickUp()
    {
        _isPicking = true;
        _animator.Play("revovlerPickup");
        _source.PlayOneShot(AudioClips[2]);
        yield return new WaitForSeconds(2f);
        _isPicking = false;
        _logoText.gameObject.SetActive(false);
        enemies.SetActive(true);
        playerStats.SetActive(true);

    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _currMagazineSize = MagazineSize;
    }
    private void Update()
    {
        bulelts.text = "Bullets: " + MagazineSize + "/" + _currMagazineSize;
        if (PauseMenu.IsPaused || DialogueSystem.IsDialogue)
        {
            _source.Stop();
            return;
        }
    

        if (!_isShoot && !_isReload && !_isPicking)
        {
            if (Input.GetButton("Fire1") && _currMagazineSize >= 1)
            {
                StopAllCoroutines();
                StartCoroutine(C_Shoot());
            }
            else if (_currMagazineSize <= 0)
            {
                StopAllCoroutines();
                StartCoroutine(C_Reload());
            }
            else if (Input.GetKeyDown(KeyCode.R) && _currMagazineSize < MagazineSize)
            {
                StopAllCoroutines();
                StartCoroutine(C_Reload());
            }
            else
            {
                _animator.Play("REVOLVER_idle");
            }
        }
    }
   private void DealDamage()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, Distance))
        {
           GhostBehaviour enemy = _hit.collider.GetComponent<GhostBehaviour>();
           if (enemy != null)
            { 
               enemy.TakeDamage(Damage);
            }
        }
    }
   private IEnumerator C_Reload()
    {
        _isReload = true;
        _currMagazineSize = MagazineSize;
        _source.PlayOneShot(AudioClips[1]);
        _animator.Play("REVOLVER_reload");
        yield return new WaitForSeconds(reloadTime);
        _isReload = false;
    }
    private IEnumerator C_Shoot()
    {
        _isShoot = true;

        _currMagazineSize--;

        StartCoroutine(cameraShake.C_Shake(shakeDuration,magntitude));

        DealDamage();

        _animator.Play("REVOLVER_fire");

        _source.PlayOneShot(AudioClips[0]);

        yield return new WaitForSeconds(shootDelays[0]);

        _isShoot = false;
    }



}
