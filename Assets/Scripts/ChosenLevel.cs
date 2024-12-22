using UnityEngine;

public static class ChosenLevel
{
    static ChosenLevel()
    {
        Data = Resources.Load<LevelData>("Data/Levels/level_1");
    }

    public static LevelData Data { get; set; }
}