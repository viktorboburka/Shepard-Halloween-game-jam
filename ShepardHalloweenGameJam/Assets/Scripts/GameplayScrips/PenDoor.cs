using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenDoor : MonoBehaviour
{
    private FlockAgent _flockAgent;
    // Start is called before the first frame update

    private SpawnManager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("koliduju");
        if (other.tag == "Sheep")
        {
            FlockAgent flockAgent = other.transform.GetComponent<FlockAgent>();
            //yield return new WaitForSeconds(2f);
            //flockAgent.IsInPen();
            Destroy(other.gameObject);
            _spawnManager.SpawnSpriteInPen();
        }
    }

    
}