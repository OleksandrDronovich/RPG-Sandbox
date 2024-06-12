using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        // Use this for initialization
        void Start()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}%", health.GetPercentage());
        }
    }
}