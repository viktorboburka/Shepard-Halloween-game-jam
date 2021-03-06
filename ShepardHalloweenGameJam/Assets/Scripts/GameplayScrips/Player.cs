using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;
    private bool _hooing = false;
    [SerializeField]
    private AudioClip _hooHoo;
    [SerializeField]
    private AudioSource _playerAudioSource;
    //private Wolf _wolf;
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange = 1.0f;
    [SerializeField]
    private LayerMask _enemyLayers;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _dogPrefab;
    private int _maxDogsSpawned = 3;
   
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private Animator _hooAnim;


    // Start is called before the first frame update
    void Start()
    {
        _playerAudioSource.clip = _hooHoo;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        //_anim = GameObject.Find("ScytheSwing1").GetComponent<Animator>();
        _hooAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        if (Input.GetKey(KeyCode.Space))
        {
            DoHoo();

        }
        //_hooAnim.SetBool("Hoo", false);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoHooSound();
            
        }
        if (Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0))
        { 
            killWolf();
        }
        if ((Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(1)) && _maxDogsSpawned > 0)
        {
            Instantiate(_dogPrefab, transform.position + new Vector3(-1.07f, 0, 0), Quaternion.identity);
            _maxDogsSpawned--;
        }
    }

    void calculateMovement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(new Vector2(horizontalInput, verticalInput) * _speed * Time.deltaTime);

        //transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.8f, 7.8f), Mathf.Clamp(transform.position.y, -3.8f, 3.8f));

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalInput, verticalInput) * _speed;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            TurnRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            TurnLeft();
        }
    }

    public void TurnRight()
    {
        if(transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void TurnLeft()
    {
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

    public void DoHoo()
    {
        _hooing = true;
        _hooAnim.SetTrigger("HooTrigger");

    }
    public void DoHooSound()
    {
        _playerAudioSource.Play();
    }

    public Vector2 PlayerPos()
    {
        return transform.position;
    }

    public void killWolf()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);
        foreach (Collider2D enemy in enemies)
        {
            if(enemy.tag == "Wolf")
            {
                Destroy(enemy.gameObject);
            }
            
        }
        _anim.SetTrigger("Swipe");

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
    

