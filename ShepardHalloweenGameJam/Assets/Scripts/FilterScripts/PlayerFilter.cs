using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/PlayerLayer")]
public class PlayerFilter : ContextFilter {
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original) {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original) {
            /*if (item.gameObject.layer == LayerMask.NameToLayer("Player")) {
                filtered.Add(item);
            }*/
            if (mask == (mask | (1 << item.gameObject.layer))) {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
