using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleButton : MonoBehaviour
{
    private TilemapRenderer obstacleTilemapRenderer;
    private TilemapCollider2D obstacleTilemapCollider;
    private bool playerInButton;
    private IEnumerator coroutine;
    private Animator buttonAnim;
    private bool buttonPressed;
    
    // Start is called before the first frame update
    void Start()
    {
        obstacleTilemapRenderer = GameObject.Find("ObstacleTilemap").GetComponent<TilemapRenderer>();
        obstacleTilemapCollider = GameObject.Find("ObstacleTilemap").GetComponent<TilemapCollider2D>();
        playerInButton = false;
        buttonAnim = GetComponent<Animator>();
        buttonPressed = false;
    }

    /*IEnumerator Wait()
    {
        //new WaitForSeconds(5);
        obstacleTilemapRenderer.enabled = true;
        obstacleTilemapCollider.enabled = true;
    }*/
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInButton && !buttonPressed)
        {
            
            /*coroutine = Wait();
            StartCoroutine(coroutine);*/
            StartCoroutine(DisableObstacles());
            
        }
    }
    
    private IEnumerator DisableObstacles()
    {
        buttonPressed = true;
        obstacleTilemapCollider.gameObject.SetActive(false);
        // obstacleTilemapRenderer.enabled = false;
        // obstacleTilemapCollider.enabled = false;
        buttonAnim.SetBool("buttonPressing", true);
        buttonAnim.Play("ButtonPress");
        buttonAnim.StopPlayback();
        yield return new WaitForSeconds(5);
        
        buttonAnim.SetBool("buttonPressing", false);
        obstacleTilemapCollider.gameObject.SetActive(true);
        // obstacleTilemapRenderer.enabled = true;
        // obstacleTilemapCollider.enabled = true;
        buttonPressed = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInButton = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInButton = false;
        }
    }
}
