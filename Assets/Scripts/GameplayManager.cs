using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private Field _field;
    [SerializeField] private DotsConnector _connecter;
    [SerializeField] private GameplaySceneUI _ui;
    [SerializeField] private EndPanel _endPanel;
    [SerializeField] private ScoreView _scoreView;

    private LevelData _levelData;
    private int _movesCount;
    private TargetDotData[] _targetDotDatas;
    private ScoreCounter _scoreCounter;

    public event Action<int> MovesCountChanged;

    private void Start()
    {
        _levelData = ChosenLevel.Data;
        _movesCount = _levelData.MovesCount;
        _targetDotDatas = new TargetDotData[_levelData.TargetDotDatas.Length];

        for (int i = 0; i < _levelData.TargetDotDatas.Length; i++)
        {
            _targetDotDatas[i] = new TargetDotData();
            _targetDotDatas[i].DotData = _levelData.TargetDotDatas[i].DotData;
            _targetDotDatas[i].Count = _levelData.TargetDotDatas[i].Count;
        }

        _scoreCounter = new ScoreCounter();
        _scoreView.Initialize(_scoreCounter);

        MovesCountChanged?.Invoke(_movesCount);

        _field.Generate(_levelData.GridData);
        _connecter.DotsConnected += OnDotsConnected;
        _connecter.Activate();

        _ui.SpawnTargetDotViews(_targetDotDatas);

        _sceneChanger.PlayDisappearAnimation();
    }

    private void OnDisable()
    {
        _connecter.DotsConnected -= OnDotsConnected;
    }

    public void SetPause(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0f;
            _connecter.Deactivate();
        }
        else
        {
            Time.timeScale = 1f;
            _connecter.Activate();
        }
    }

    private void OnDotsConnected(Dot[] dots)
    {
        _movesCount--;

        MovesCountChanged?.Invoke(_movesCount);

        foreach (Dot dot in dots)
        {
            foreach (TargetDotData targetDotData in _targetDotDatas)
            {
                if (dot.Data == targetDotData.DotData)
                {
                    targetDotData.ReduceCount();
                }
            }
        }

        _ui.UpdateTargetDotViews(_targetDotDatas);

        _scoreCounter.Increase(dots.Length);

        if (IsLevelCompleted())
        {
            _connecter.Deactivate();
            _ui.Lock();
            _endPanel.UpdateView(true);

            StartCoroutine(ShowEndPanel());
        }
        else
        {
            if (_movesCount <= 0)
            {
                _movesCount = 0;
                _connecter.Deactivate();
                _ui.Lock();
                _endPanel.UpdateView(false);

                StartCoroutine(ShowEndPanel());
            }
        }
    }

    private bool IsLevelCompleted()
    {
        foreach (TargetDotData targetDotData in _targetDotDatas)
        {
            if (targetDotData.Count > 0)
            {
                return false;
            }
        }

        return true;
    }

    private IEnumerator ShowEndPanel()
    {
        yield return new WaitForSeconds(0.5f);

        _endPanel.Appear();
    }
}