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

    // Start is called before the first frame update
    void Start()
    {
        _playerAudioSource.clip = _hooHoo;
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        if (Input.GetKey(KeyCode.Space))
        {
            DoHoo();

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoHooSound();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            killWolf();
        }
    }

    void calculateMovement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(new Vector2(horizontalInput, verticalInput) * _speed * Time.deltaTime);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.8f, 7.8f), Mathf.Clamp(transform.position.y, -3.8f, 3.8f));

        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalInput, verticalInput) * _speed;

    }

    public void DoHoo()
    {
        _hooing = true;
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
        Debug.Log("jsem tu");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);
        foreach (Collider2D enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
