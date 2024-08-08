using System.Collections.Generic;
using SIUE.ControllerGames.Audio;
using UnityEngine;
using UnityEngine.Assertions;

namespace SIUE.ControllerGames.DataBase
{
    [CreateAssetMenu(fileName = "AudioDB", menuName = "DB/AudioDB")]
    public class AudioDB : ScriptableObject
    {
        [field: SerializeField] public List<EachAudio> audios { get; private set; }

        private Dictionary<EAudio, EachAudio> audiosDict = new Dictionary<EAudio, EachAudio>();
        public void Initialized()
        {
            foreach (var audio in audios)
            {
                if (!audiosDict.ContainsKey(audio.EAudio))
                {
                    audiosDict.Add(audio.EAudio, audio);
                }
            }
        }
        public EachAudio GetAudio(EAudio eAudio)
        {
            if (audiosDict.ContainsKey(eAudio))
                return audiosDict[eAudio];
            return null;
        }
        void OnValidate()
        {
            foreach (var eachAudio in audios)
            {
                eachAudio.OnValidate(name);
            }
            Dictionary<EAudio, EachAudio> audiosDict = new Dictionary<EAudio, EachAudio>();
            foreach (var audio in audios)
            {
                if (!audiosDict.ContainsKey(audio.EAudio))
                    audiosDict.Add(audio.EAudio, audio);
                else
                    Debug.LogError($"{audio.EAudio} is already present in the dictionary");
            }
        }
    }
}
