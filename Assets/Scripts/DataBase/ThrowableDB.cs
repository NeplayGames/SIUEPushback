using System.Collections;
using System.Collections.Generic;
using SIUE.ControllerGames.Throwables;
using UnityEngine;
using UnityEngine.Assertions;

namespace SIUE.ControllerGames.DataBase
{
  [CreateAssetMenu(fileName = "Throwable DB", menuName = "DB/ThrowableDB")]
  public class ThrowableDB : ScriptableObject
  {

    [field: SerializeField] public GameObject throwable { get; private set; }
    [field: SerializeField] public Color commonColor  { get; private set; }
    [field: SerializeField] public Color rareColor  { get; private set; }
    [field: SerializeField] public Color epicColor  { get; private set; }
    [field: SerializeField] public Color legendaryColor  { get; private set; }
    void OnValidate()
    {
       Assert.IsNotNull(throwable);
    }
  }
}
