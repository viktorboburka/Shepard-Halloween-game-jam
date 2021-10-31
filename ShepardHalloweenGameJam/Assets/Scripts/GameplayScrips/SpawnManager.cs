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
            Instantiate(_wolfPrefab, new Vector3(Random.Range(-35f, 35f), 18, 0), Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
        }
    }

  
    
    
}
