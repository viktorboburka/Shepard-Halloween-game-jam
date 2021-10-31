using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    private Transform _player;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private bool left, right;
    private Transform _flockAgent;
    private int findSheepNow;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _rb = this.GetComponent<Rigidbody2D>();
        right = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(findSheepNow < 10)
        {
            float currentSheep = Mathf.Random(0.10);
            _flockAgent = GameObject.Find("Agent" + 0).GetComponent<Transform>();
            if(_flockAgent.position.x > 50)
            {

            }
            
        }
        findSheepNow++;
        Vector3 direction = _flockAgent.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        direction.Normalize();
        _movement = direction;
        if (transform.position.y <= -5f)
        {
            Destroy(this.gameObject);
        }
        if(direction.x > 0)
        {
            turnRight();
        }
        if(direction.x < 0)
        {
            turnLeft();
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

    public void turnLeft()
    {
        if (left)
        {
            return;
        }
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        left = true;
        right = false;
    }
    public void turnRight()
      
    {
        if (right)
        {
            return;
        }
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        left = false;
        right = true; 
    }
}
