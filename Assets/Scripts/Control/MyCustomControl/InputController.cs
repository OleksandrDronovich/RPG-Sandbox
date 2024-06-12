using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance;
    public GameObject inputCanvas;
    public Joystick movingJoystick;
    public Joystick rotateJoystick;

    // Start is called before the first frame update
    void Awake()
    {
#if UNITY_STANDALONE_WIN 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputCanvas.SetActive(false);
#elif PLATFORM_ANDROID
        inputCanvas.SetActive(true);
#endif
        Instance = this;
    }

    public Vector2 GetMovingVector()
    {
        Vector2 moving = Vector2.zero;
        if(!inputCanvas.activeSelf || inputCanvas == null)
            moving = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        else
            moving = new Vector2(movingJoystick.Horizontal, movingJoystick.Vertical);

        return moving;
    }

    public Vector2 GetRotatingVector()
    {
        Vector2 rotating = Vector2.zero;
        if(!inputCanvas.activeSelf || inputCanvas == null)
            rotating = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        else
            rotating = new Vector2(rotateJoystick.Horizontal, rotateJoystick.Vertical);

        return rotating;
    }
}