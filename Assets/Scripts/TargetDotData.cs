using UnityEngine;
using System;

[Serializable]
public class TargetDotData
{
    [field: SerializeField] public DotData DotData { get; private set; }
    [field: SerializeField] public int Count { get; private set; }
}