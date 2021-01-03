using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public Inventory inventory;
    public GameObject itemButton;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        Debug.Log(inventory);
    }


    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("It worked");
            for (int i = 0; i < inventory.slots.Length; i++) {
                if (!inventory.isFull[i]) {

                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
    
}
