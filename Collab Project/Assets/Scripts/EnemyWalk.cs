using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public float speed = 5f;
    public float maxWalkDistance = 5f;
    
    private float _distanceWalked;
    
    // Start is called before the first frame update
    void Start()
    {
           StartCoroutine(Walk());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SlimeTrail"))
        {
            SwitchDirections();
        }
    }

    private void SwitchDirections()
    {
        speed *= -1;
        _distanceWalked = 0f;
    }

    IEnumerator Walk()
    {
        while (true)
        {
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            transform1.localScale = localScale;
            while (System.Math.Abs(_distanceWalked) < maxWalkDistance)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
                _distanceWalked += speed * Time.deltaTime;
                if(Physics2D.Raycast(transform.position, Vector2.right*speed, 0.5f, LayerMask.GetMask("Ground")))
                {
                    SwitchDirections();
                }
                yield return null;
            }
            SwitchDirections();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
