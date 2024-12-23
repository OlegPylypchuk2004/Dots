using System;
using System.Collections;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private Field _field;
    [SerializeField] private DotsConnector _connecter;
    [SerializeField] private GameplaySceneUI _ui;
    [SerializeField] private EndPanel _endPanel;

    private LevelData _levelData;
    private int _movesCount;
    private TargetDotData[] _targetDotDatas;

    public event Action<int> MovesCountChanged;

    private void Start()
    {
        _levelData = ChosenLevel.Data;
        _movesCount = _levelData.MovesCount;
        _targetDotDatas = _levelData.TargetDotDatas;

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

        if (_movesCount <= 0)
        {
            //  if not losed

            _movesCount = 0;
            _connecter.Deactivate();
            _ui.Lock();
            _endPanel.UpdateView(false);

            StartCoroutine(ShowEndPanel());
        }
    }

    private IEnumerator ShowEndPanel()
    {
        yield return new WaitForSeconds(0.5f);

        _endPanel.Appear();
    }
}