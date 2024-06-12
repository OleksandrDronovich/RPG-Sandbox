using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Resources;
using System;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 2f;
        [SerializeField] float waypointDwellTime = 3f;
        [Range(0f, 1f)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] bool isAgressive;


        Health health;
        Fighter fighter;
        Mover mover;
        GameObject player;

        Vector3 guardLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Start()
        {
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");

            guardLocation = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;
            if (isAgressive)
            {
                if (InAttackRange() && fighter.CanAttack(player))
                {

                    AttackBehavior();
                }
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceArrivedAtWaypoint += Time.deltaTime;
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardLocation;

            if(patrolPath != null)
            {
                if (AtWaypoint()) 
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if(timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
            
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private bool InAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}