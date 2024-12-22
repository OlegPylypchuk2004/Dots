using System;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private Field _field;
    [SerializeField] private DotsConnector _connecter;

    private LevelData _levelData;
    private int _movesCount;

    public event Action<int> MovesCountChanged;

    private void Start()
    {
        _levelData = ChosenLevel.Data;
        _movesCount = _levelData.MovesCount;

        _field.Generate(_levelData.GridData);
        _connecter.DotsConnected += OnDotsConnected;

        _sceneChanger.PlayDisappearAnimation();
    }

    private void OnDisable()
    {
        _connecter.DotsConnected -= OnDotsConnected;
    }

    private void OnDotsConnected(Dot[] dots)
    {
        _movesCount--;

        MovesCountChanged?.Invoke(_movesCount);
    }
}