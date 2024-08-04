using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIUE.ControllerGames.Configs
{
    [CreateAssetMenu(fileName ="Player Config", menuName = "Config/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
       [field:SerializeField] public int initialPlayerSpeed {get; private set;}
    }

}
