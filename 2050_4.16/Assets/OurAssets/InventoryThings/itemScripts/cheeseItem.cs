using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseItem : MonoBehaviour, Item {

    public GameObject prefab;
    public Inventory inventory;
    public Transform player;

    public void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void drop() {
        Instantiate(prefab, player);
    }
    
    public void use() {
        Debug.Log("Monch Monch");
    }
}
