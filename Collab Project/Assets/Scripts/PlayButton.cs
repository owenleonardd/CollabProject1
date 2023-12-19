using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayButton : MonoBehaviour
{
    public GameObject uiElement;

    // Start is called before the first frame update
    public void PlayGame(String levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
