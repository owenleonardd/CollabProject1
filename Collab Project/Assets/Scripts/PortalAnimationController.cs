using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAnimationController : MonoBehaviour
{
    private Animator PortalAnimator;

    // Start is called before the first frame update
    void Start()
    {
        PortalAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PortalAnimator.SetBool("playerOnPortal", true);

        }
        //throw new NotImplementedException();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PortalAnimator.SetBool("playerOnPortal", false);
        }
        //throw new NotImplementedException();
    }
}
