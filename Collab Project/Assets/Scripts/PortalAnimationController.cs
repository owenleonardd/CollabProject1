using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAnimationController : MonoBehaviour
{
    private Animator PortalAnimator;

    private bool playerInPortal;
    // Start is called before the first frame update
    void Start()
    {
        PortalAnimator = GetComponent<Animator>();
        playerInPortal = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInPortal)
        {
            Debug.Log("Player in portal and e key pressed");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PortalAnimator.SetBool("playerOnPortal", true);
            playerInPortal = true;

        }
        //throw new NotImplementedException();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PortalAnimator.SetBool("playerOnPortal", false);
            playerInPortal = false;
        }
        //throw new NotImplementedException();
    }
}
