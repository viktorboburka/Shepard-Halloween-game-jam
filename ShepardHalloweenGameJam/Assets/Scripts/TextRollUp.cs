using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRollUp : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rectTransform;
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.Translate(Vector2.up * 10 * Time.deltaTime);
    }
}
