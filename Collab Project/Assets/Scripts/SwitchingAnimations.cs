using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchingAnimations : MonoBehaviour
{
    private Animator anim;

    
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        anim = GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            anim.SetBool("facingRight", true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            anim.SetBool("facingRight", false);
        }
    }
}
