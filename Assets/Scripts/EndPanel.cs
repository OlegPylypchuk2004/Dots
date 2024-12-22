using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : Panel
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private TextMeshProUGUI _titleTextMesh;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonTextMesh;

    protected override void SubscribeOnEvents()
    {
        base.SubscribeOnEvents();

        _button.onClick.AddListener(OnButtonClicked);
    }

    protected override void UnsubscribeOnEvents()
    {
        base.UnsubscribeOnEvents();

        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void UpdateView(bool isLevelCompleted)
    {
        if (isLevelCompleted)
        {
            _titleTextMesh.text = "Level completed";
            _buttonTextMesh.text = "Continue";
        }
        else
        {
            _titleTextMesh.text = "The moves are over";
            _buttonTextMesh.text = "Try again";
        }
    }

    private void OnButtonClicked()
    {
        _sceneChanger.LoadCurrent();
    }
}