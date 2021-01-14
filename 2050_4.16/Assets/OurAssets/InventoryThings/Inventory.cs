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
    public GameObject bombPrefab;
    public GameObject ladderPrefab;
    public GameObject allEnemies;
    public AudioClip useItemClip;
    public AudioClip useARGlassesClip;

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
                hit.collider.SendMessage("LoadScene");
                AudioSource.PlayClipAtPoint(useItemClip, transform.position, 0.5f);
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0.0f){
            if(Input.GetAxis("Mouse ScrollWheel") > 0.0f){
                itemSelected += 1;
            }
            if(Input.GetAxis("Mouse ScrollWheel") < 0.0f){
                itemSelected -= 1;
            }
        }

        //Drop if equipped and "Q" is pressed
        if (Input.GetKeyDown(KeyCode.Q)) {
            Drop();
            AudioSource.PlayClipAtPoint(useItemClip, transform.position, 0.5f);
        }

        //Use if equipped and "F" is pressed
        if (Input.GetKeyDown(KeyCode.F)) {
            Use();
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
        AudioSource.PlayClipAtPoint(useItemClip, transform.position, 0.5f);
        if (itemButtons[itemSelected].name == "CheeseIcon(Clone)") {
            Debug.Log("Monch Monch");
            slotScript slotData = slots[itemSelected].GetComponent<slotScript>();
            itemButtons[itemSelected] = null;
            slotData.drop();
            isFull[itemSelected] = false;
        }

        if (itemButtons[itemSelected].name == "BombIcon(Clone)") {
            Rigidbody rb = Instantiate(bombPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(camera.TransformDirection(Vector3.forward) * 32f, ForceMode.Impulse);
            rb.AddForce(camera.TransformDirection(Vector3.up) * 8f, ForceMode.Impulse);
            slotScript slotData = slots[itemSelected].GetComponent<slotScript>();
            itemButtons[itemSelected] = null;
            slotData.drop();
            isFull[itemSelected] = false;
        }

        if (itemButtons[itemSelected].name == "ARGlassesIcon(Clone)") {
            allEnemies.BroadcastMessage("ARGlassesActivated");
            AudioSource.PlayClipAtPoint(useARGlassesClip, transform.position, 0.5f);
            slotScript slotData = slots[itemSelected].GetComponent<slotScript>();
            itemButtons[itemSelected] = null;
            slotData.drop();
            isFull[itemSelected] = false;
        }

        if (itemButtons[itemSelected].name == "LadderIcon(Clone)") {
            Rigidbody rb = Instantiate(ladderPrefab, transform.position + transform.forward, Quaternion.AngleAxis(10, camera.right) * camera.rotation).GetComponent<Rigidbody>();
            slotScript slotData = slots[itemSelected].GetComponent<slotScript>();
            itemButtons[itemSelected] = null;
            slotData.drop();
            isFull[itemSelected] = false;
        }


    }
}
