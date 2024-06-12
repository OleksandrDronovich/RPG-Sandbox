using RPG.Saving;
using RPG.Stats;
using System;
using System.Collections;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveble
    {
        [SerializeField] float experiencePoints;

        //public delegate bool ExperienceGainedDelegate();
        public event Action onExperienceGained;

        public void GainExperience(float exp) 
        {
            experiencePoints += exp;
            onExperienceGained();
        }

        public float GetPoints()
        {
            return experiencePoints ;
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}