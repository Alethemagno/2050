using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public int iLevelToLoad;
    public GameObject door;
    public Vector3 doorPosition;
    public Quaternion doorRotation;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    void LoadScene()
    {
        //door.position = doorPosition;
        Destroy(door);
        SceneManager.LoadScene(iLevelToLoad);
    }

}