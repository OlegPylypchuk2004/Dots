using UnityEngine;

[CreateAssetMenu (fileName = "DotData", menuName = "Data/Dot")]
public class DotData : ScriptableObject
{
    [field: SerializeField] public Color[] Colors { get; private set; }
}