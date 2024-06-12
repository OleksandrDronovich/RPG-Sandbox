using RPG.Combat;
using RPG.Core;
using RPG.FPS.Combat;
using RPG.Movement;
using RPG.Resources;
using UnityEngine;


namespace RPG.Control
{
    public class FirstPersonalController : MonoBehaviour
    {
        [SerializeField] float movingSpeed;
        [SerializeField] float rotatingSpeed;

        Rigidbody rigidbody;
        FirstPersonalMover mover;
        Health health;

        private void Start()
        {
            mover = GetComponent<FirstPersonalMover>();
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                rotatingSpeed = 1;
            if (Input.GetKeyDown(KeyCode.Alpha2))
                rotatingSpeed = 2;
            if (Input.GetKeyDown(KeyCode.Alpha3))
                rotatingSpeed = 3;
            if (Input.GetKeyDown(KeyCode.Alpha4))
                rotatingSpeed = 4;

            if (health.IsDead()) return;
            //InteractWithCombat();
            InteractWithRotating();
            InteractWithMovement();
            //ResetVelosity();
            print("Nothing to do");
        }

        //private void InteractWithCombat()
        //{
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            GetComponent<FirstPersonalFighter>().Attack();
        //        }
        //}

        void ResetVelosity() 
        {
            if(GetComponent<Rigidbody>().velocity.y != 0) 
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        private void InteractWithRotating()
        {
            Vector3 rotateVector = Vector3.zero;
            float rotateSpeed = 0;
            if(GetVectorFromRotation() != Vector3.zero) 
            {
                rotateVector = GetVectorFromRotation();
                rotateSpeed = rotatingSpeed;
            }
            mover.StartRotateAction(rotateVector, rotateSpeed);
        }

        private void InteractWithMovement()
        {
            Vector3 moveVector = Vector3.zero;
            float moveSpeed = 0;
            if (GetPositionFromMove() != Vector3.zero)
            {
                moveVector = GetPositionFromMove();
                moveSpeed = movingSpeed * Time.deltaTime;
                if (Input.GetKey(KeyCode.LeftShift))
                    moveSpeed = movingSpeed * 2 * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * 200);
            }

            mover.StartMoveAction(moveVector, moveSpeed);
        }

        public Vector3 GetPositionFromMove()
        {
            Vector2 inputVector = InputController.Instance.GetMovingVector();
            return new Vector3(inputVector.x, 0, inputVector.y);
        }

        public Vector3 GetVectorFromRotation() 
        {
            Vector2 inputVector = InputController.Instance.GetRotatingVector();
            return new Vector3(-inputVector.y, inputVector.x, 0);
        }
    }
}