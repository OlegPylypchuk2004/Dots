using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private Field _field;

    private LevelData _levelData;

    private void Start()
    {
        _levelData = ChosenLevel.Data;

        _field.Generate(_levelData.GridData);

        _sceneChanger.PlayDisappearAnimation();
    }
}