using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Configs;
using UnityEngine;

namespace SIUE.ControllerGames.DataBase
{
    [CreateAssetMenu(fileName = "ConfigsDB", menuName = "DB/ConfigsDB")]
    public class ConfigsDB : ScriptableObject
    {
        [field: Header("Player Config")]
        [field: SerializeField] public PlayerConfig playerConfig { get; private set; }
    }

}
