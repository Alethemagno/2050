using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class ContinueScript : MonoBehaviour
{
    public static event Action<string> ButtonPressed = delegate { };
    public int iLevelToLoad;

    void LoadScene() 
    {
        SceneManager.LoadScene(iLevelToLoad);
    }
    

    // Start is called before the first frame update
    public void ButtonClicked()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Loading Scene " + iLevelToLoad);
        LoadScene();
    }
}
