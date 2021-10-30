using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior {

    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
        //no neighbors no change
        if (context.Count == 0) {
            return Vector2.zero;
        }
        //add all points and get average
        Vector2 cohesionMove = Vector2.zero;

        //using filter to get average
        List<Transform> filteredContext;
        if (filter == null) {
            filteredContext = context;
        }
        else {
            filteredContext = filter.Filter(agent, context);
        }
        foreach (Transform item in filteredContext) {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent, not global pos
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}
