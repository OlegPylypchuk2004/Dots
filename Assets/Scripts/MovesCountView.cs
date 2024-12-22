using TMPro;
using UnityEngine;

public class MovesCountView : MonoBehaviour
{
    [SerializeField] private GameplayManager _gameplayManager;
    [SerializeField] private TextMeshProUGUI _textMesh;

    private void OnEnable()
    {
        _gameplayManager.MovesCountChanged += OnMovesCountChanged;
    }

    private void OnDisable()
    {
        _gameplayManager.MovesCountChanged -= OnMovesCountChanged;
    }

    private void OnMovesCountChanged(int movesCount)
    {
        _textMesh.text = movesCount.ToString();
    }
}