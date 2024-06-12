using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject 
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        Dictionary<CharacterClass, Dictionary<Stats, float[]>> lookupTable = null;

        public float GetStat(Stats stat, CharacterClass characterClass, int level) 
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];

            if (levels.Length < level - 1) 
            {
                return 0;
            }

            return levels[level - 1];
        }

        public int GetLevels(Stats stat, CharacterClass characterClass) 
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stats, float[]>> ();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stats, float[]>();

                foreach (ProgressionStat progressionStat in progressionClass.stats) 
                {
                    statLookupTable[progressionStat.stats] = progressionStat.levels;
                }

                lookupTable[progressionClass.characterClass] = statLookupTable;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass 
        {
            public CharacterClass characterClass;
            public ProgressionStat[]  stats;

        }

        [System.Serializable]
        class ProgressionStat 
        {
            public Stats stats;
            public float[] levels;
        }
    }
}