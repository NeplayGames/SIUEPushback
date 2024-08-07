using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Configs;
using UnityEngine;
using UnityEngine.Assertions;

namespace SIUE.ControllerGames.DataBase
{
    [CreateAssetMenu(fileName = "ConfigsDB", menuName = "DB/ConfigsDB")]
    public class ConfigsDB : ScriptableObject
    {
        [field: Header("Player Config")]
        [field: SerializeField] public PlayerConfig playerConfig { get; private set; }
        [field: SerializeField] public ThrowableItemConfig throwableItemConfig { get; private set; }
        [field: SerializeField] public float surfaceHeight { get; private set; }

        void OnValidate()
        {
            Assert.IsNotNull(playerConfig);
            Assert.IsNotNull(throwableItemConfig);
        }
    }

}
