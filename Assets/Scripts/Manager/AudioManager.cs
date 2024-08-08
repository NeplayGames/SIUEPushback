
using SIUE.ControllerGames.DataBase;
using UnityEngine;

namespace SIUE.ControllerGames.Audio
{
    public class AudioManager
    {
        private AudioDB audioDB { get; }

        public AudioManager(AudioDB audioDB)
        {
            this.audioDB = audioDB;
            this.audioDB.Initialized();
        }

        public void PlayOneShot(AudioSource audioSource, EAudio eAudio)
        {
            EachAudio eachAudio = audioDB.GetAudio(eAudio);
            audioSource.clip = eachAudio.AudioClip;
            audioSource.loop = eachAudio.LoopAudio;
            audioSource.pitch = Random.Range(eachAudio.MinPitch, eachAudio.MaxPitch);
            audioSource.PlayOneShot(audioSource.clip);
        }
       
    }

}
