using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySceneUI : MonoBehaviour
{
    [SerializeField] private GameplayManager _gameplayManager;
    [SerializeField] private CanvasGroup _mainCanvasGroup;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Panel _pausePanel;
    [SerializeField] private TargetDotView _targetDotViewPrefab;
    [SerializeField] private Transform _targetDotsParent;

    private List<TargetDotView> _targetDotViews;

    private void Awake()
    {
        _targetDotViews = new List<TargetDotView>();
    }

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
    }

    public void SpawnTargetDotViews(TargetDotData[] targetDotDatas)
    {
        foreach (TargetDotData targetDotData in targetDotDatas)
        {
            TargetDotView targetDotView = Instantiate(_targetDotViewPrefab, _targetDotsParent);
            targetDotView.Initialize(targetDotData.DotData.Color, targetDotData.Count);

            _targetDotViews.Add(targetDotView);
        }
    }

    public void Lock()
    {
        _mainCanvasGroup.interactable = false;
    }

    private void OnPauseButtonClicked()
    {
        _gameplayManager.SetPause(true);
        _pausePanel.Appear();
    }
}