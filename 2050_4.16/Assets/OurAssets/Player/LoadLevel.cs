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
    public bool doneFirstChallenge;
    public GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (player.GetComponent<PlayerMovement>().doneFirstChallenge) {
            Destroy(door);
        }
    }

    void LoadScene()
    {
        //door.position = doorPosition;
        SceneManager.LoadScene(iLevelToLoad);
        Destroy(door);
    }

}