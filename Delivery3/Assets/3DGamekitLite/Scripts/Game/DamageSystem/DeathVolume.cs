using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{
    [RequireComponent(typeof(Collider))]
    public class DeathVolume : MonoBehaviour
    {
        public new AudioSource audio;
        private Timer timer;

        private void Start()
        {
            timer = FindObjectOfType<Timer>();
        }

        void OnTriggerEnter(Collider other)
        {
            var pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.Die(new Damageable.DamageMessage());
                StartCoroutine(DataAnalytics.AddData(new UserDeaths(), pc.transform.position.ToString(), timer.GetCurrentTime(), DataAnalytics.GetDamageType(name).ToString()));
            }
            if (audio != null)
            {
                audio.transform.position = other.transform.position;
                if (!audio.isPlaying)
                    audio.Play();
            }
        }

        void Reset()
        {
            if (LayerMask.LayerToName(gameObject.layer) == "Default")
                gameObject.layer = LayerMask.NameToLayer("Environment");
            var c = GetComponent<Collider>();
            if (c != null)
                c.isTrigger = true;
        }

    }
}
