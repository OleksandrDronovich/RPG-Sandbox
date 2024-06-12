using RPG.Core;
using RPG.Resources;
using UnityEngine;

namespace RPG.Movement
{
    public class FirstPersonalMover : MonoBehaviour, IAction
    {
        [SerializeField] Transform camera;
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float maxAngleRotation;
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            this.enabled = !health.IsDead();

            //UpdateAnimator();
        }

        public void StartRotateAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            RotateTo(destination, speedFraction);
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        //private void UpdateAnimator()
        //{

        //    Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        //    float speed = localVelocity.z;
        //    GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        //}

        public void Cancel()
        {
            Debug.Log("Player canceled");
        }

        public void RotateTo(Vector3 destination, float speedFraction)
        {
            //Camera rotating
            Vector3 currentDestination = destination;
            //if (currentDestination.x < 0 && camera.eulerAngles.x > maxAngleRotation)
            //    currentDestination = destination;
            //if (currentDestination.x > 0 && camera.eulerAngles.x < maxAngleRotation)
            //    currentDestination = destination;
            //else
            //    currentDestination = Vector3.zero;
            //print(camera.eulerAngles.x);
            Vector3 cameraRotate = new Vector3(currentDestination.x, 0, 0);


            //Player rotating
            Vector3 playerRotate = new Vector3(0, destination.y, 0);

            camera.Rotate(cameraRotate, speedFraction);
            transform.Rotate(playerRotate, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            transform.Translate(destination * speedFraction);
        }
    }
}