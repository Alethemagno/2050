using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public int itemSelected;
    public GameObject[] slots, itemButtons;
    public Transform player, camera;
    public LayerMask itemLayerMask;

    private int pickUpRange;
    private RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        pickUpRange = 10;
        itemSelected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (itemSelected < 0) {
            itemSelected = 4;
        } 
        if (itemSelected > 4) {
            itemSelected = 0;
        }
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if(Input.GetKeyDown(KeyCode.E)) {
            if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward), out hit, pickUpRange, itemLayerMask)) {
                hit.collider.SendMessage("PickUp");
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0.0f){
            if(Input.GetAxis("Mouse ScrollWheel") > 0.0f){
                itemSelected += 1;
            }
            if(Input.GetAxis("Mouse ScrollWheel") < 0.0f){
                itemSelected -= 1;
            }   
            Debug.Log(itemSelected);
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
        slotScript slotData = slots[itemSelected].GetComponent<slotScript>();
        itemButtons[itemSelected] = null;
        slotData.drop();
        isFull[itemSelected] = false;
    }

    void Use() {
        itemButtons[itemSelected] = null;
        slots[itemSelected] = null;
        isFull[itemSelected] = false;
    }
}
