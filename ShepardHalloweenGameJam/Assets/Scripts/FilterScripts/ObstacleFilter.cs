using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Flock/Filter/ObstacleLayer")] 
public class ObstacleFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original) {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original) {
            if (mask == (mask | (1 << item.gameObject.layer))) {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
