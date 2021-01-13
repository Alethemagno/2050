using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    public GameObject correctForm;
    private bool moving;
    public int index;

    public GameObject button; 

    private float startPosX;
    private float startPosY;

    private Vector3 resetPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        resetPosition = this.transform.localPosition;
    }

    void Update()
    {
        if (moving)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x -
                     startPosX, mousePos.y - startPosY,
                     this.gameObject.transform.localPosition.z);
        }

    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }

    }

    private void OnMouseUp()
    {
        moving = false;

        if (Mathf.Abs(this.transform.localPosition.x - correctForm.transform.localPosition.x ) <= 1f &&
            Mathf.Abs(this.transform.localPosition.y - correctForm.transform.localPosition.y) <= 1f)
        {
            this.transform.localPosition = new Vector3(correctForm.transform.localPosition.x, correctForm.transform.localPosition.y,
                correctForm.transform.localPosition.z);

            button.GetComponent<LoadNextLevel>().keyArray[index] = true; 

        } 
    }
}
