using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PushTheButton : MonoBehaviour
{
    public static event Action<string> ButtonPressed = delegate { };

    private int dividerPosition;
    private string buttonName, buttonValue;

    //Start is called before the first frame
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        buttonName = gameObject.name;
        dividerPosition = buttonName.IndexOf("_");
        buttonValue = buttonName.Substring(0, dividerPosition);

        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);

    }

    private void ButtonClicked()
    {
        ButtonPressed(buttonValue);
        Debug.Log("Hello");
    }

}
