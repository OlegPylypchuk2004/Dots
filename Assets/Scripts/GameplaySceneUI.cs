using UnityEngine;
using UnityEngine.UI;

public class GameplaySceneUI : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
    }

    private void OnRestartButtonClicked()
    {
        _sceneChanger.LoadCurrent();
    }
}