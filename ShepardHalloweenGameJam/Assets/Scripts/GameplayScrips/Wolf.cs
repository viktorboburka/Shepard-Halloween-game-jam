using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    private Transform _player;
    private Rigidbody2D _rb;
    private Vector2 _movement;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = _player.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        direction.Normalize();
        _movement = direction;
        if (transform.position.y <= -5f)
        {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        moveCharacter(_movement);
    }
    void moveCharacter(Vector2 direction)
    {
        _rb.MovePosition((Vector2)transform.position + (direction * 2 * Time.deltaTime));
    }
}
