using UnityEngine;
using UnityEngine.Assertions;

namespace SIUE.ControllerGames.Audio
{
    [System.Serializable]
    public class EachAudio
    {
       [field:SerializeField] public AudioClip AudioClip {get; private set;}
       [field:SerializeField, Range(0, 1)] public float MinPitch {get; private set;}
       [field:SerializeField, Range(0, 1)] public float MaxPitch {get; private set;}
       [field:SerializeField] public EAudio EAudio{get; private set;}
       [field:SerializeField] public bool LoopAudio{get; private set;}

       public void OnValidate(string fileName)
       {
            Assert.IsNotNull(AudioClip, $"Audioclip cannot be null in {fileName}");
       }
    }

}
