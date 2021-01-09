using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotScript : MonoBehaviour
{
    public void drop() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
