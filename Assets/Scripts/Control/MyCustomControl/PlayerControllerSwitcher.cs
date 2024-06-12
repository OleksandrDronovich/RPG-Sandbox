using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Control <T> where T:Behaviour
{
    [SerializeField] T controller;
    [SerializeField] GameObject cameraController;

    public void StateControl(bool state) 
    {
        controller.enabled = state;
        cameraController.SetActive(state);
    }
}

public class PlayerControllerSwitcher : MonoBehaviour
{
    [SerializeField] Control<ThirdPlayerController> actionControl;
    [SerializeField] Control<PlayerController> diabloControl;
    bool isActionControl;
    public GameObject swichCameraButton;
    public void SwichControl() 
    {
        isActionControl = !isActionControl;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            isActionControl = true;    
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            isActionControl = false;
        }

        swichCameraButton.SetActive(isActionControl);
        actionControl.StateControl(isActionControl);
        diabloControl.StateControl(!isActionControl);
    }
}