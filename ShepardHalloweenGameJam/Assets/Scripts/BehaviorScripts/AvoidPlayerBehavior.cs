using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/PlayerAvoidance")]
public class AvoidPlayerBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock) {
        //no neighbors no change
        if (context.Count == 0) {
            return Vector2.zero;
        }
        //add all points and get average
        Vector2 avoidanceMove = Vector2.zero;
        int nToAvoid = 0;
        foreach (Transform item in context) {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquarePlayerAvoidanceRadius) {
                nToAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }
        if (nToAvoid > 0) {
            avoidanceMove /= nToAvoid;
        }

        return avoidanceMove;
    }
}
