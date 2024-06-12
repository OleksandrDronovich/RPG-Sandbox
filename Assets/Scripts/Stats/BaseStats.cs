using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        int currentLevel = 0;

        private void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();

            if (experience != null) 
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private bool OnStuffDone(float value) 
        {
            print(value);
            return true;
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel) 
            {
                currentLevel = newLevel;
                print("LevelUP");
            }
        }

        public float GetStat(Stats stat) 
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel() 
        {
            if(currentLevel < 1) CalculateLevel();
            return currentLevel;
        }

        public int CalculateLevel() 
        {
            Experience experience = GetComponent<Experience>();

            if (experience == null) return startingLevel;

            float currentEXP = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stats.ExperienceToLevelUp, characterClass);

            for (int levels = 1; levels <= penultimateLevel; levels++) 
            {
                float XPToLevelUp = progression.GetStat(Stats.ExperienceToLevelUp, characterClass, levels);
                if (XPToLevelUp > currentEXP) 
                {
                    return levels;
                }
            }
            return penultimateLevel + 1;
        }
    }
}