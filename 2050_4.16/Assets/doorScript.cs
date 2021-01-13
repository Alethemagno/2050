using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public GameObject player;
    private float counter;

    void Start() {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Renderer>().enabled = false;
        counter = 0;
    }
    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 20.0f) {
            gameObject.GetComponent<Collider>().enabled = true;
            gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
