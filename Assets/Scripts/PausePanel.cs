using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : Panel
{
    [SerializeField] private GameplayManager _gameplayManager;
    [SerializeField] private SceneChanger _sceneChanger;

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;

    protected override void SubscribeOnEvents()
    {
        base.SubscribeOnEvents();

        _continueButton.onClick.AddListener(OnContinueButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonButtonClicked);
        _menuButton.onClick.AddListener(OnMenuButtonButtonClicked);
    }

    protected override void UnsubscribeOnEvents()
    {
        base.UnsubscribeOnEvents();

        _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        _restartButton.onClick.RemoveListener(OnRestartButtonButtonClicked);
        _menuButton.onClick.RemoveListener(OnMenuButtonButtonClicked);
    }

    private void OnContinueButtonClicked()
    {
        Disappear()
            .OnComplete(() =>
            {
                _gameplayManager.SetPause(false);
            });
    }

    private void OnRestartButtonButtonClicked()
    {
        _gameplayManager.SetPause(false);
        _sceneChanger.LoadCurrent();
    }

    private void OnMenuButtonButtonClicked()
    {
        _gameplayManager.SetPause(false);
        _sceneChanger.LoadByName("ChooseLevelScene");
    }
}