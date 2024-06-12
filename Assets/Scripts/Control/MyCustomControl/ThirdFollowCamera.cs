using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdFollowCamera : MonoBehaviour
{
    [SerializeField] GameObject firstViewCamera;
    [SerializeField] GameObject thirdViewCamera;
    bool isFirstView;

    [SerializeField] Transform playerTransform;

    public void SwichViewCamera() 
    {
        isFirstView = !isFirstView;
        transform.rotation = playerTransform.rotation;
        firstViewCamera.SetActive(isFirstView);
        thirdViewCamera.SetActive(!isFirstView);
    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) SwichViewCamera();
    }
    public Quaternion GetCameraRotation()
    {
        return transform.rotation;
    }

    void LateUpdate()
    {
        if (isFirstView) 
        {
            transform.rotation = playerTransform.rotation;
        }
        else
        {
            transform.Rotate(0, InputController.Instance.GetRotatingVector().x * 3, 0);
        }
        transform.position = playerTransform.position;
    }
  }