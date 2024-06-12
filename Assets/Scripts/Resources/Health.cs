using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveble
    {
        [SerializeField] float healthPoints = -1;
        bool isDead = false;

        private void Start()
        {
            if(healthPoints < 0)
            healthPoints = GetComponent<BaseStats>().GetStat(Stats.Stats.Health);
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instagator, float damage) 
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
            {
                Die();
                AwardExperience(instagator);
            }
        }

        private void AwardExperience(GameObject instagator)
        {
            Experience experience = instagator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stats.Stats.ExperienceReward));
        }

        public float GetPercentage() 
        {
            return 100 *( healthPoints / GetComponent<BaseStats>().GetStat(Stats.Stats.Health));
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints <= 0) Die();
        }
    }
}