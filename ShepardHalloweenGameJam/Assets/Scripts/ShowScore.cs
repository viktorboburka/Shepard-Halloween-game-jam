using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void textChange (int sheepSaved, int totalSheep) {
        Debug.Log("textchange");
        text.text = "Congratulations, you saved " + sheepSaved + " sheep out of " + totalSheep + "!";
    }
}
