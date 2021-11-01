using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flock : MonoBehaviour
{

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;
    private GameObject player;
    private GameObject pen;
    private float sceneEndTime = -1f;
    private bool sceneOver = false;

    [Range(0, 500)]
    public int startingCount = 250;
    public const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 5f;
    [Range(1f, 100f)]
    public float maxSpeed = 3f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;
    //[Range(1f, 1000f)]
    public float playerAvoidanceRadius = 3f;
    public float playerAvoidanceWeight = 5f;
    public float dogAvoidanceRadius = 7f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    float squarePlayerAvoidanceRadius;
    public float SquarePlayerAvoidanceRadius { get { return squarePlayerAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        squarePlayerAvoidanceRadius = playerAvoidanceRadius * playerAvoidanceRadius;

        for (int i = 0; i < startingCount; i++) {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent" + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);


        }

        player = GameObject.Find("Player");
        pen = GameObject.Find("PenDoor");
    }

    // Update is called once per frame
    void Update() {

        

        GameObject[] dogs = GameObject.FindGameObjectsWithTag("Dog");
        foreach (FlockAgent agent in agents) {
            List<Transform> context = GetNearbyObjects(agent); //what exists in the context of the neighborradius

            //for testing getnearbyobjects, works if you dont use sprite with set color
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            
            ////player repel
            Vector2 playerRepel = (Vector2) (agent.transform.position - player.transform.position);

            if (playerRepel != Vector2.zero && playerRepel.magnitude < playerAvoidanceRadius && Input.GetKey("space")) {
                if (playerRepel.sqrMagnitude > playerAvoidanceWeight * playerAvoidanceWeight) {
                    playerRepel.Normalize();
                    playerRepel *= playerAvoidanceWeight;
                }

                move += playerRepel;
            }

            ////dog repel
            if (dogs.Length != 0) {
                foreach (GameObject dog in dogs) {
                    Vector2 dogRepel = (Vector2)(agent.transform.position - dog.transform.position);

                    if (dogRepel != Vector2.zero && dogRepel.magnitude < dogAvoidanceRadius) {
                        if (dogRepel.sqrMagnitude > playerAvoidanceWeight * playerAvoidanceWeight) {
                            dogRepel.Normalize();
                            dogRepel *= playerAvoidanceWeight;
                        }
                        move += dogRepel;
                    }

                }
            }

            ////pen door attract
            Vector2 penAttract = (Vector2)(agent.transform.position - pen.transform.position) * 2;
            if (penAttract != Vector2.zero && playerRepel.magnitude < playerAvoidanceRadius/2) {
                if (penAttract.sqrMagnitude > playerAvoidanceWeight * playerAvoidanceWeight * 3) {
                    penAttract.Normalize();
                    penAttract *= playerAvoidanceWeight * 3;
                }
                move -= penAttract;
            }
            
            //set speed
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed) {
                move = move.normalized * maxSpeed;
            }
            agent.FlockMove(move);
        }

        float deadAgents = 0;
        foreach (FlockAgent agent in agents) {
            if (agent.transform.position.x > 50 && agent.transform.position.y > 50) {
                deadAgents++;
            }
        }

        if (deadAgents >= startingCount) {
            Debug.Log("scene end: " + sceneEndTime + "current time: " + Time.time);
            if (!sceneOver) {
                GameObject canvas = GameObject.Find("Canvas");
                ShowScore ss = canvas.GetComponent<ShowScore>();

                PenDoor pd = pen.GetComponent<PenDoor>();
                int sheepSaved = pd._numberOfSheepInPen;

                ss.textChange(sheepSaved, startingCount);
                sceneEndTime = Time.time + 5f;
            }
            sceneOver = true;
            if (Time.time > sceneEndTime) {
                if (SceneManager.GetActiveScene().buildIndex == 2) {
                    SceneManager.LoadScene(0);
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    List<Transform> GetNearbyObjects(FlockAgent agent) {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders) {
            if (c != agent.AgentCollider) {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
