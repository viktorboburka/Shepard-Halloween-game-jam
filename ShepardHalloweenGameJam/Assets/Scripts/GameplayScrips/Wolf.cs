using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    private PenDoor _penDoor;
    private Transform _player;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private bool left, right;
    private Transform _flockAgent;
    private int findSheepNow;
    int currentSheep = Random.Range(0, 10);
    float dogAvoidanceRadius;
    float dogAvoidanceWeight;
    SpriteRenderer _spriteRenderer;
    float spawnedTime;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _rb = this.GetComponent<Rigidbody2D>();
        right = true;
        _flockAgent = GameObject.Find("Agent" + currentSheep).GetComponent<Transform>();
        dogAvoidanceRadius = 5f;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.flipX = true;
        spawnedTime = Time.time;
        _penDoor = GameObject.Find("PenDoor").GetComponent<PenDoor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < spawnedTime + 1) {
            return;
        }

        if(_flockAgent.position.x > 50)
        {
            GameObject[] sheepGameObjects = GameObject.FindGameObjectsWithTag("Sheep");
            currentSheep = Random.Range(0, sheepGameObjects.Length);
            _flockAgent = GameObject.Find("Agent" + currentSheep).GetComponent<Transform>();

            /*currentSheep++;
            _flockAgent = GameObject.Find("Agent" + currentSheep).GetComponent<Transform>();
            Debug.Log(currentSheep);
            if (currentSheep > 10)
            {
                currentSheep = Random.Range(0, 10);
            }*/
        }
        
        Vector2 direction = _flockAgent.position - transform.position;
        direction.Normalize();
        //float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        //Debug.Log("direction before dog: " + direction);

        GameObject[] dogs = GameObject.FindGameObjectsWithTag("Dog");
        if (dogs.Length != 0) {
            foreach (GameObject dog in dogs) {
                Vector2 dogRepel = (Vector2) (transform.position - dog.transform.position);

                if (dogRepel.magnitude < dogAvoidanceRadius) {
                    dogRepel.Normalize();
                    direction += dogRepel;
                }
            }
        }

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        if (obstacles.Length != 0) {
            foreach (GameObject obstacle in obstacles) {
                Vector2 obstacleRepel = (Vector2)(transform.position - obstacle.transform.position);

                if (obstacleRepel.magnitude < dogAvoidanceRadius) {
                    obstacleRepel.Normalize();
                    direction += obstacleRepel;
                }
            }
        }

        //Debug.Log("direction after dog: " + direction);

        direction.Normalize();
        _movement = direction;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Sheep")
        {
            other.transform.position =  new Vector3(100, 100, 0);
            _penDoor._numberOfSheepKilled++;
        }
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
