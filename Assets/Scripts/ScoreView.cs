using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;

    private ScoreCounter _scoreCounter;
    private bool _isInitialized;

    private void OnDestroy()
    {
        if (_isInitialized)
        {
            _scoreCounter.ValueChanged -= OnValueChanged;
        }
    }

    public void Initialize(ScoreCounter scoreCounter)
    {
        _scoreCounter = scoreCounter;
        _scoreCounter.ValueChanged += OnValueChanged;

        _isInitialized = true;
    }

    private void OnValueChanged(int value)
    {
        _textMesh.text = $"{value:D9}";
    }
}