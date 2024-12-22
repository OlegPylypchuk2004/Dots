using UnityEngine;

[CreateAssetMenu (fileName = "LevelData", menuName = "Data/Level")]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public GridData GridData { get; private set; }
}