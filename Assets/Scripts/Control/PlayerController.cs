using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Resources;
using UnityEngine;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        private void Start()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;
            if(InteractWithMovement()) return;
            print("Nothing to do");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) 
                { 
                    continue; 
                }

                if (Input.GetMouseButtonDown(0)) 
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                GetComponent<Mover>().StartMoveAction(GetPositionFromMove(), 1);
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        public Vector3 GetPositionFromMove()
        {
            Vector2 inputVector = InputController.Instance.GetMovingVector();
            return transform.position + Quaternion.Euler(0, -45, 0) * new Vector3(-inputVector.x, 0, -inputVector.y);
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}