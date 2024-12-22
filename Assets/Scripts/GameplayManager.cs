using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private Field _field;

    private void Start()
    {
        _field.Generate();

        _sceneChanger.PlayDisappearAnimation();
    }
}