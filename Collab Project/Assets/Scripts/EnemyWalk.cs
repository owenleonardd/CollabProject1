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
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            float distanceWalked = 0f;
            while (distanceWalked < maxWalkDistance)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
                distanceWalked += speed * Time.deltaTime;
                if(transform.localScale.x < 0 && Physics2D.Raycast(transform.position, Vector2.right*speed, 0.5f, LayerMask.GetMask("Ground")))
                {
                    speed *= -1;
                }
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
