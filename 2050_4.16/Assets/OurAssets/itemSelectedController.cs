using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSelectedController : MonoBehaviour
{
    private Vector3 pos;
    private float xPos;
    private float reference;

    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        xPos = transform.position.x;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        reference = inventory.slots[inventory.itemSelected].transform.position.x;
        transform.position = new Vector3(reference, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
