using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFloat : MonoBehaviour
{
    // Start is called before the first frame update
    public float floatStrength = 1;
    public float floatSpeed = 1;
    private float originalY;
    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        position = new Vector3(position.x,
            originalY + ((float)Mathf.Sin(Time.time * floatSpeed) * floatStrength),
            position.z);
        transform.position = position;
    }
}
