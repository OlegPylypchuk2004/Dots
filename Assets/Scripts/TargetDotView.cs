using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetDotView : MonoBehaviour
{
    [SerializeField] private Image _previewImage;
    [SerializeField] private TextMeshProUGUI _countTextMesh;

    private int _targetCount;

    public void Initialize(Color targetColor, int targetCount)
    {
        _previewImage.color = targetColor;
        _targetCount = targetCount;

        _countTextMesh.text = $"{0}/{_targetCount}";
    }

    public void UpdateView(int currentCount)
    {
        if (currentCount < 0)
        {
            currentCount = 0;
        }

        currentCount = _targetCount - currentCount;
        _countTextMesh.text = $"{currentCount}/{_targetCount}";
    }
}