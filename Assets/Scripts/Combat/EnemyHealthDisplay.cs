using RPG.Resources;
using System;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        // Use this for initialization
        void Start()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            if(fighter.GetTarget() == null)
            {
                GetComponent<TextMeshProUGUI>().text = "N/A";
                return;
            }
            Health health = fighter.GetTarget();
            string healthString = String.Format("{0:0}%", health.GetPercentage());
            GetComponent<TextMeshProUGUI>().text = healthString;
        }
    }
}