using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private Image _lockIcon;
    [SerializeField] private GameObject _line;
    [SerializeField] private Button _button;

    private int _levelNumber;
    private bool _isInitialized;

    public event Action<int> Clicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void Initialize(int levelNumber, bool isOpened, bool isLastLevel = false)
    {
        _levelNumber = levelNumber;
        _numberText.text = levelNumber.ToString();

        if (isOpened)
        {
            _numberText.gameObject.SetActive(true);
            _lockIcon.gameObject.SetActive(false);
        }
        else
        {
            _numberText.gameObject.SetActive(false);
            _lockIcon.gameObject.SetActive(true);
        }

        _button.interactable = isOpened;
        _line.SetActive(!isLastLevel);

        _isInitialized = true;
    }

    private void OnButtonClicked()
    {
        if (!_isInitialized)
        {
            return;
        }

        Clicked?.Invoke(_levelNumber);
    }
}