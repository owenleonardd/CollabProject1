using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    
    private bool playerInPortal = false;
    public Image fadeImage;
    
    private FadeInOut fadeScript;
    
    private void Start()
    {
        fadeScript = fadeImage.GetComponent<FadeInOut>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInPortal = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInPortal = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInPortal)
        {
            StartCoroutine(FadeToScene());
        }
        
    }

    public IEnumerator FadeToScene()
    {
        fadeScript.FadeIn();
        yield return new WaitForSeconds(1/fadeScript.fadeSpeed);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
