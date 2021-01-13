﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = 15f;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f, jumpHeight;
    public LayerMask groundMask;
    public Vector3 lastCheckpoint;
    public GameObject[] checkpoints;

    Vector3 velocity;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        lastCheckpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lastCheckpoint);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = 0f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;
        //Vector3 move = new Vector3 (x, 0f, z);

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump")) {
            velocity.y = jumpHeight;
        }
    }

    void playerDeath() {
        Debug.Log("Ure dead");
        controller.enabled = false;
        controller.transform.position = lastCheckpoint;
        controller.enabled = true;
        Debug.Log("Ure dead so you were sent to " + lastCheckpoint);
    }
}
