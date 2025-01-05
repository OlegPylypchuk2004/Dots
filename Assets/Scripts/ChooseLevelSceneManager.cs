using UnityEngine;

public class ChooseLevelSceneManager : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private LevelButton _levelButtonPrefab;
    [SerializeField] private Transform _levelButtonsParent;

    private LevelData[] _levelDatas;
    private LevelButton[] _levelButtons;

    private void Start()
    {
        PlayerData playerData = PlayerDataManager.LoadPlayerData();
        _levelDatas = Resources.LoadAll<LevelData>("Data/Levels");
        _levelButtons = new LevelButton[_levelDatas.Length];

        for (int i = 0; i < _levelButtons.Length; i++)
        {
            LevelButton levelButton = Instantiate(_levelButtonPrefab, _levelButtonsParent);
            levelButton.Initialize(i + 1, i < playerData.OpenedLevelsCount, i == _levelButtons.Length - 1);
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

        ChosenLevel.Data = _levelDatas[levelNumber - 1];

        _sceneChanger.LoadByName("LevelScene");
    }
}