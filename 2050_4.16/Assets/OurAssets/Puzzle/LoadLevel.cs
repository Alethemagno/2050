using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LoadLevel : MonoBehaviour
{
    public int iLevelToLoad;
    //public MoveSystem isDoorOpen;
    public bool[] keyArray;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(iLevelToLoad);
    }

    void Update()
    {
        if (keyArray[0] == true && keyArray[1] == true && keyArray[2] == true && keyArray[3] == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            LoadScene();
        }

    }

    /**
    private void ButtonClicked()
    {
        Debug.Log("hello");
        if ( keyArray[0] == true && keyArray[1] == true && keyArray[2] == true && keyArray[3] == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            LoadScene();
        }
    }
    */

}

