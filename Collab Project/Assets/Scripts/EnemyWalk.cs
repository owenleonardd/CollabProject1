using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public float speed = 5f;
    public float maxWalkDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {
           StartCoroutine(Walk());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Walk()
    {
        while (true)
        {
            var transform1 = transform;
            var localScale = transform1.localScale;
            localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            transform1.localScale = localScale;
            float distanceWalked = 0f;
            while (System.Math.Abs(distanceWalked) < maxWalkDistance)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
                distanceWalked += speed * Time.deltaTime;
                if(Physics2D.Raycast(transform.position, Vector2.right*speed, 0.5f, LayerMask.GetMask("Ground")))
                {
                    speed *= -1;
                }
                yield return null;
            }
            speed *= -1;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
