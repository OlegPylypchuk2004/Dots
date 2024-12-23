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

        UpdateView(0);
    }

    public void UpdateView(int currentCount)
    {
        _countTextMesh.text = $"{currentCount}/{_targetCount}";
    }
}