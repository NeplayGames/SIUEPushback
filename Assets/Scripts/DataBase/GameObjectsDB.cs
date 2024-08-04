using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Player;
using UnityEngine;
using UnityEngine.Assertions;

namespace SIUE.ControllerGames.DataBase
{
    [CreateAssetMenu(fileName = "Game Object DataBase", menuName = "DB/GameObjectDB")]
    public class GameObjectsDB : ScriptableObject
    {
        [field: SerializeField] public List<GameObject> players { get; private set; }
        [field: SerializeField] public ThrowableDB throwableDB { get; private set; }
        void OnValidate()
        {
            Assert.IsNotNull(throwableDB, $"The {nameof(throwableDB)} is null in {nameof(GameObjectsDB)}");
            foreach (var player in players)
                Assert.IsTrue(player.GetComponent<PlayerController>(), "The player needs to have the player Controller");
        }
    }
}
