﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public GameObject player;
    public float spinSpeed;
    private bool activated;
    public Light myLight;

    void Start() {
        myLight.intensity = 0.0f;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed);
        Vector3 distanceToPlayer = player.transform.position - transform.position;
        if (distanceToPlayer.magnitude < 1.6f) {
            Debug.Log("--- Checkpoint Set ---" + transform.position);
            player.GetComponent<PlayerMovement>().lastCheckpoint = transform.position;
            if (!activated) {
                spinSpeed *= 6;
                activated = true;
                myLight.intensity = 3.0f;

            }
        }
    }
}
