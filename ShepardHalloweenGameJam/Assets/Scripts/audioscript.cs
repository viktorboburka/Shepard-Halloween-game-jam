using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioscript : MonoBehaviour

  
{

    private AudioSource _audioSource;
    [SerializeField]
    private AudioSource _audioSource2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitThanPlay());
        _audioSource = GetComponent<AudioSource>();
        //_audioSource2 = GameObject.Find("AudioSource1").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waitThanPlay()
    { 
        _audioSource2.Play();
        yield return new WaitForSeconds(6f);
        
        _audioSource.Play();
    }
}
