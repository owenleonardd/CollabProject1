using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public int minY;
    private Animator anim;

    private void Start()
    {
        minY = Math.Abs(minY);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Math.Abs(transform.position.y) > minY)
        {
            Die();
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("OBSTACLE");
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("dying", true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
