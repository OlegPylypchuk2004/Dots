using UnityEngine;
using System;

[Serializable]
public class TargetDotData
{
    [field: SerializeField] public DotData DotData { get; set; }
    [field: SerializeField] public int Count { get; set; }

    public void ReduceCount()
    {
        Count--;
    }
}