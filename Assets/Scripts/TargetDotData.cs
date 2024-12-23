using UnityEngine;
using System;

public class TargetDotData
{
    [field: SerializeField] public DotData DotData { get; private set; }
    [field: SerializeField] public int Count { get; private set; }
}