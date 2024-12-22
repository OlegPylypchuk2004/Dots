using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private RectTransform _circleRectTransform;
    [SerializeField] private RectTransform _backgroundRectTransform;
    [SerializeField] private RectTransform _fadeRectTransform;
    [SerializeField] private Image _fadeImage;

    private float _maxCircleSize;

    private AsyncOperation _loadSceneOperation;

    private void Awake()
    {
        _circleRectTransform.sizeDelta = Vector2.zero;
        _backgroundRectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        _fadeRectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        _maxCircleSize = Mathf.Max(Screen.width, Screen.height) * 1.5f;
    }

    public void LoadByName(string sceneName)
    {
        _loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        _loadSceneOperation.allowSceneActivation = false;

        StartCoroutine(DelayBeforeLoad());
    }

    public void LoadByIndex(int sceneIndex)
    {
        _loadSceneOperation = SceneManager.LoadSceneAsync(sceneIndex);
        _loadSceneOperation.allowSceneActivation = false;

        StartCoroutine(DelayBeforeLoad());
    }

    public void LoadCurrent()
    {
        _loadSceneOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        _loadSceneOperation.allowSceneActivation = false;

        StartCoroutine(DelayBeforeLoad());
    }

    public void LoadPrevious()
    {
        _loadSceneOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
        _loadSceneOperation.allowSceneActivation = false;

        StartCoroutine(DelayBeforeLoad());
    }

    public void LoadNext()
    {
        _loadSceneOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        _loadSceneOperation.allowSceneActivation = false;

        StartCoroutine(DelayBeforeLoad());
    }

    private IEnumerator DelayBeforeLoad()
    {
        if (_eventSystem != null)
        {
            _eventSystem.gameObject.SetActive(false);
        }

        PlayAppearAnimation();

        yield return new WaitForSeconds(0.5f);

        _loadSceneOperation.allowSceneActivation = true;
    }

    public void PlayAppearAnimation()
    {
        if (_eventSystem != null)
        {
            _eventSystem.gameObject.SetActive(false);
        }

        _circleRectTransform.gameObject.SetActive(true);

        Sequence appearSequence = DOTween.Sequence();

        appearSequence.Join
            (_circleRectTransform.DOSizeDelta(Vector2.zero, 0.5f)
            .From(new Vector2(_maxCircleSize, _maxCircleSize))
            .SetEase(Ease.InQuad));

        appearSequence.Join
            (_fadeImage.DOFade(1f, 0.5f)
            .From(0f)
            .SetEase(Ease.InQuad));

        appearSequence.SetLink
            (gameObject);

    }

    public void PlayDisappearAnimation()
    {
        _circleRectTransform.gameObject.SetActive(true);
        _eventSystem.gameObject.SetActive(false);

        Sequence disappearSequence = DOTween.Sequence();

        disappearSequence.Join
            (_circleRectTransform.DOSizeDelta(new Vector2(_maxCircleSize, _maxCircleSize), 0.5f)
            .From(Vector2.zero)
            .SetEase(Ease.OutQuad));

        disappearSequence.Join
            (_fadeImage.DOFade(0f, 0.5f)
            .From(1f)
            .SetEase(Ease.OutQuad));

        disappearSequence.SetLink(gameObject);

        disappearSequence
            .OnComplete(() =>
            {
                _circleRectTransform.gameObject.SetActive(false);

                if (_eventSystem != null)
                {
                    _eventSystem.gameObject.SetActive(true);
                }
            });
    }
}