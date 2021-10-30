using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    

   
    [SerializeField]
    private GameObject _wolfPrefab;
 
    void Start()
    {
        StartCoroutine(SpawnAnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnAnEnemy()
    {
        while (true)
        {
            Instantiate(_wolfPrefab, new Vector3(Random.Range(-8f, 8f), 4, 0), Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }
}