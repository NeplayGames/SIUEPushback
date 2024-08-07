using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIUE.ControllerGames.Configs
{
    [CreateAssetMenu(fileName ="Player Config", menuName = "Config/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
       [field:SerializeField, Range(0, 100)] public float playerSpeed {get; private set;}
       [field:SerializeField, Range(0, 360)] public int playerRotationSpeed {get; private set;}
       [field:SerializeField, Range(0, 10)] public int totalPlayer {get; private set;}
    }

}
