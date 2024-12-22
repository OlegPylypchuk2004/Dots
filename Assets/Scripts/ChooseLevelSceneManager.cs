using UnityEngine;

public class ChooseLevelSceneManager : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private LevelButton _levelButtonPrefab;
    [SerializeField] private Transform _levelButtonsParent;

    private LevelButton[] _levelButtons;

    private void Start()
    {
        _levelButtons = new LevelButton[10];

        for (int i = 0; i < _levelButtons.Length; i++)
        {
            LevelButton levelButton = Instantiate(_levelButtonPrefab, _levelButtonsParent);
            levelButton.Initialize(i + 1, i == _levelButtons.Length - 1);
            levelButton.Clicked += OnLevelButtonClicked;

            _levelButtons[i] = levelButton;
        }

        _sceneChanger.PlayDisappearAnimation();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            _levelButtons[i].Clicked -= OnLevelButtonClicked;
        }
    }

    private void OnLevelButtonClicked(int levelNumber)
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            _levelButtons[i].Clicked -= OnLevelButtonClicked;
        }

        _sceneChanger.LoadByName("LevelScene");
    }
}