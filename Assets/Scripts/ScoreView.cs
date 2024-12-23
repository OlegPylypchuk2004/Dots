using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;

    private ScoreCounter _scoreCounter;
    private bool _isInitialized;
    private int _currentValue;

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
        DOTween.Kill(_currentValue);

        DOTween.To(() => _currentValue, x => _currentValue = x, value, 0.25f)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                _textMesh.text = $"{_currentValue:D9}";
            })
            .OnKill(() =>
            {
                _currentValue = value;
            });
    }
}