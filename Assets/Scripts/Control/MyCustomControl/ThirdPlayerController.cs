using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPlayerController : MonoBehaviour
{
    [SerializeField] Mover movementController;
    [SerializeField] ThirdFollowCamera followCamera;
    [SerializeField] float walkSpeed;

    void Update()
    {
        float speed;
        if (Input.GetKey(KeyCode.LeftShift))
            speed = walkSpeed * 3;
        else speed = walkSpeed;
        movementController.StartMoveAction(GetPositionFromMove(), speed);
    }

    public Vector3 GetPositionFromMove() 
    {
        Vector2 inputVector = InputController.Instance.GetMovingVector();
        return transform.position + followCamera.GetCameraRotation() * new Vector3(inputVector.x, 0, inputVector.y);
    }
}