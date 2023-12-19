using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFloat : MonoBehaviour
{
    // Start is called before the first frame update
    public float floatStrength = 1;
    public float floatSpeed = 1;
    private float originalY;
    private RectTransform _transform;
    
    void Start()
    {
        _transform = GetComponent<RectTransform>();
        this.originalY = _transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        _transform.position += Vector3.up * (Mathf.Sin(Time.time * floatSpeed) * floatStrength);
    }
}
