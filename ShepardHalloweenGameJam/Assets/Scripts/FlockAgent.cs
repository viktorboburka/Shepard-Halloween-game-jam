using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{

    [SerializeField]
    private bool isInPen;

    Flock agentFlock;
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

    public void Move(Vector2 velocity) {
        //if (!isDead) transform.up = velocity;
        transform.up = velocity;
        if (isInPen) velocity = Vector2.zero;
        transform.position += (Vector3) velocity * Time.deltaTime;
    }
}
