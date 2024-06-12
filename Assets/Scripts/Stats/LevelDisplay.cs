using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        // Use this for initialization
        void Start()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<TextMeshProUGUI>().text = String.Format("{0:0}", baseStats.GetLevel());
        }
    }
}