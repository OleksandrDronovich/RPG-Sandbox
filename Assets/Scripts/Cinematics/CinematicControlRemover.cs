using RPG.Cinematic;
using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            GetComponent<PlayableDirector>().stopped += EnableControl;
            GetComponent<PlayableDirector>().played += DisableControl;

            player = GameObject.FindWithTag("Player");
        }
        private void DisableControl(PlayableDirector pd)
        {
            
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent <PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd) 
        {
            print("EnableControl");
            player.GetComponent<PlayerController>().enabled = false;
        }
    }
}