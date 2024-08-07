using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIUE.ControllerGames.Configs
{
    [CreateAssetMenu(fileName ="Throwable Item Config", menuName = "Config/Throwable Item Config" )]
    public class ThrowableItemConfig : ScriptableObject
    {
    [field: SerializeField] public Color commonColor  { get; private set; }
    [field: SerializeField] public Color rareColor  { get; private set; }
    [field: SerializeField] public Color epicColor  { get; private set; }
    [field: SerializeField] public Color legendaryColor  { get; private set; }

    [field: SerializeField, Range(4,30)] public float commonDistancePushed  { get; private set; }
    [field: SerializeField, Range(4,30)] public float rareDistancePushed  { get; private set; }
    [field: SerializeField, Range(4,30)] public float epicDistancePushed  { get; private set; }
    [field: SerializeField, Range(4,30)] public float legendaryDistancePushed  { get; private set; }
    [field: SerializeField, Range(40,150)] public float commonItemSpeed  { get; private set; }
    [field: SerializeField, Range(40,150)] public float rareItemSpeed  { get; private set; }
    [field: SerializeField, Range(40,150)] public float epicItemSpeed  { get; private set; }
    [field: SerializeField, Range(40,150)] public float legendaryItemSpeed  { get; private set; }

    [field: SerializeField, Range(1,10)] public int throwableRate  { get; private set; }
    [field: SerializeField, Range(1,30)] public int totalThrowableItems  { get; private set; }
    [field: SerializeField, Range(1,30)] public float ArenaLength  { get; private set; }

    }

}
