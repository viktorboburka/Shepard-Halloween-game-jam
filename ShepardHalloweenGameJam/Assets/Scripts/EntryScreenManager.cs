using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryScreenManager : MonoBehaviour
{

    //private AssetBundle assetBundle;
    //private string[] scenePaths;
    // Start is called before the first frame update
    void Start()
    {
        //assetBundle = AssetBundle.LoadFromFile("Assets/Scenes");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space")) {
            Debug.Log("return pressed");
            //SceneManager.LoadScene("TutorialScene", LoadSceneMode.Additive);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
