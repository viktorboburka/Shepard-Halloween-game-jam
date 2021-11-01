using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public float spawnTime = 10f;
    public float toSpawn = 1f;
    private float spawned = 0f;
   
    [SerializeField]
    private GameObject _wolfPrefab;
   
  

  
 
    void Start()
    {
       //StartCoroutine(SpawnAnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > (spawned + 1) * spawnTime && spawned < toSpawn) {
            Instantiate(_wolfPrefab, this.transform.position, Quaternion.identity);
            spawned++;
        }
        //Destroy(this);
    }

    IEnumerator SpawnAnEnemy()
    {
        while (true) {
            yield return new WaitForSeconds(10.0f);
            Instantiate(_wolfPrefab, new Vector3(Random.Range(-35f, 35f), 18, 0), Quaternion.identity);
        }
    }

  
    
    
}
