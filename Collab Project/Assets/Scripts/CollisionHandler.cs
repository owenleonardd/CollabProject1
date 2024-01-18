using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public int minY;
    private Animator anim;

    public Image fadeImage;
    private FadeInOut fadeScript;
    private void Start()
    {
        minY = Math.Abs(minY);
        anim = GetComponent<Animator>();
        fadeScript = fadeImage.GetComponent<FadeInOut>();
    }

    private void Update()
    {
        if (Math.Abs(transform.position.y) > minY)
        {
            StartCoroutine(Die());
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("OBSTACLE");
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        fadeScript.FadeIn();
        anim.SetBool("dying", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        
    }

}
