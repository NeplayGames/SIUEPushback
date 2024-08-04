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

    [field: SerializeField] public ThrowableItems throwable { get; private set; }
    [field: SerializeField] public Color commonColor  { get; private set; }
    [field: SerializeField] public Color rareColor  { get; private set; }
    [field: SerializeField] public Color epicColor  { get; private set; }
    [field: SerializeField] public Color legendaryColor  { get; private set; }

    [field: SerializeField, Range(4,20)] public float commonDistancePushed  { get; private set; }
    [field: SerializeField, Range(4,20)] public float rareDistancePushed  { get; private set; }
    [field: SerializeField, Range(4,20)] public float epicDistancePushed  { get; private set; }
    [field: SerializeField, Range(4,20)] public float legendaryDistancePushed  { get; private set; }
    [field: SerializeField, Range(40,100)] public float commonItemSpeed  { get; private set; }
    [field: SerializeField, Range(40,100)] public float rareItemSpeed  { get; private set; }
    [field: SerializeField, Range(40,100)] public float epicItemSpeed  { get; private set; }
    [field: SerializeField, Range(40,100)] public float legendaryItemSpeed  { get; private set; }
    void OnValidate()
    {
       Assert.IsNotNull(throwable);
    }
  }
}
