using UnityEngine;
using UnityEngine.UI;

public class GameplaySceneUI : MonoBehaviour
{
    [SerializeField] private GameplayManager _gameplayManager;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Panel _pausePanel;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
    }

    private void OnPauseButtonClicked()
    {
        _gameplayManager.SetPause(true);
        _pausePanel.Appear();
    }
}