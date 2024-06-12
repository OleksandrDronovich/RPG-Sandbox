using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        // Use this for initialization
        void Start()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}", experience.GetPoints());
        }
    }
}