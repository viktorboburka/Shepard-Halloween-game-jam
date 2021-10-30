using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior {
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
        //no neighbors no change
        if (context.Count == 0) {
            return Vector2.zero;
        }
        //add all points and get average
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext;
        if (filter == null) {
            filteredContext = context;
        }
        else {
            filteredContext = filter.Filter(agent, context);
        }
        foreach (Transform item in filteredContext) {
            cohesionMove += (Vector2) item.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent, not global pos
        cohesionMove -= (Vector2) agent.transform.position;
        return cohesionMove;
    }
}
