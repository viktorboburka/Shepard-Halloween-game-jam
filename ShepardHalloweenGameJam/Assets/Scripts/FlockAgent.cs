using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{

    [SerializeField]
    private bool isInPen;

    [SerializeField]
    Flock agentFlock;
    private bool left, right;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }
    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    

    public void Initialize(Flock flock) {
        agentFlock = flock;
    }

    public void FlockMove(Vector2 velocity) {
        //if (!isDead) transform.up = velocity;
        transform.up = velocity;
        if (isInPen) velocity = Vector2.zero;
        transform.position += (Vector3) velocity * Time.deltaTime;
    }

    public void ResetUpVector()
    {
        transform.up = Vector2.zero;
    }

    public void IsInPen()
    {
        isInPen = true;
        transform.position = new Vector3(100, 100, 0);
    }

    public void PlayerMove(Vector2 playerPosition) {
        
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
