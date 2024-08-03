using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace SIUE.ControllerGames.Throwables
{
    [CreateAssetMenu(fileName = "Throwable Item Config", menuName = "Config/Throwable Item Config")]
    public class ThrowableConfig : ScriptableObject
    {
        [field: SerializeField] public EThrowablesRarity eThrowablesRarity { get; private set; }
        [field: SerializeField] public GameObject throwable { get; private set; }

        void OnValidate()
        {
            Assert.IsNotNull(throwable, $"{nameof(throwable)} is null in {nameof(ThrowableConfig)}");
        }
    }    
}
