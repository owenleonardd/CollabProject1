using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleButton : MonoBehaviour
{
    private TilemapRenderer obstacleTilemapRenderer;
    private TilemapCollider2D obstacleTilemapCollider;
    private bool playerInButton;
    
    // Start is called before the first frame update
    void Start()
    {
        obstacleTilemapRenderer = GameObject.Find("ObstacleTilemap").GetComponent<TilemapRenderer>();
        obstacleTilemapCollider = GameObject.Find("ObstacleTilemap").GetComponent<TilemapCollider2D>();
        playerInButton = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInButton)
        {
            obstacleTilemapRenderer.enabled = false;
            obstacleTilemapCollider.enabled = false;
        }
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
