using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTrigger = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            { 
                if (alreadyTrigger) return;
                alreadyTrigger = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}