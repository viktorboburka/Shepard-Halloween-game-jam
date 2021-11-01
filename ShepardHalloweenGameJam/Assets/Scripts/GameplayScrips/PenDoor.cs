using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenDoor : MonoBehaviour
{
   
    private FlockAgent _flockAgent;
    // Start is called before the first frame update

    [SerializeField]
    private GameObject[] _sheepPlural;

    private int _numberOfSheepInPen = 0;
    public int maxSheep = 5;

    private SpawnManager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_numberOfSheepInPen >= maxSheep) {
            //SceneManager.LoadScene("TutorialScene", LoadSceneMode.Additive);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        Debug.Log("Sheep in pen: " + _numberOfSheepInPen);
        Debug.Log("maxSheep: " + maxSheep);
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sheep")
        {
            FlockAgent flockAgent = other.transform.GetComponent<FlockAgent>();
            yield return new WaitForSeconds(0.1f);
            flockAgent.IsInPen();
            _sheepPlural[_numberOfSheepInPen].SetActive(true);
            _numberOfSheepInPen++;
        }
    }

    
}