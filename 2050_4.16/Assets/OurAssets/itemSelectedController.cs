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
        reference = 67*inventory.itemSelected + 27;
        transform.position = new Vector3(xPos + reference, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
