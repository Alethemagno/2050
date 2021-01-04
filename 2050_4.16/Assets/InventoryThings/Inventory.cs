using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots, itemButton;
    public Transform player, camera;
    public LayerMask itemLayerMask;

    private int pickUpRange;
    private RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        pickUpRange = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if(Input.GetKeyDown(KeyCode.E)) {
            if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, pickUpRange, itemLayerMask)) {
                hit.collider.SendMessage("PickUp");
            }
        }

        //Drop if equipped and "Q" is pressed
        if (Input.GetKeyDown(KeyCode.Q)) {
            Drop();
        }
    }

    int inventoryFull() {
        for (int i = 0; i < slots.Length; i++) {
            if (!isFull[i]) {
                return i;
            } 
        }
        return -1;
    }

    void Drop() {

    }
}
