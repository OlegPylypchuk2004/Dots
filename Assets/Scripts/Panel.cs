using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    [SerializeField] private PanelFade _fade;
    [SerializeField] private RectTransform _panelRectTransform;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private CanvasGroup _contentCanvasGroup;

    public virtual Sequence Appear()
    {
        gameObject.SetActive(true);
        _contentCanvasGroup.interactable = false;
        _contentCanvasGroup.alpha = 0f;

        Sequence appearSequence = DOTween.Sequence();

        appearSequence.Join
            (_fade.Appear());

        appearSequence.Append
            (_panelRectTransform.DOScale(1f, 0.125f)
            .From(0.75f)
            .SetEase(Ease.OutQuad));

        appearSequence.Join
            (_backgroundImage.DOFade(1f, 0.125f)
            .From(0f)
            .SetEase(Ease.OutQuad));

        appearSequence.Append
            (_contentCanvasGroup.DOFade(1f, 0.125f)
            .From(0f)
            .SetEase(Ease.OutQuad));

        appearSequence.SetUpdate(true);
        appearSequence.SetLink(gameObject);

        appearSequence.OnKill(() =>
        {
            _contentCanvasGroup.interactable = true;

            SubscribeOnEvents();
        });

        return appearSequence;
    }

    public virtual Sequence Disappear()
    {
        _contentCanvasGroup.interactable = false;

        UnsubscribeOnEvents();

        Sequence disappearSequence = DOTween.Sequence();

        disappearSequence.Join
            (_contentCanvasGroup.DOFade(0f, 0.125f)
            .From(1f)
            .SetEase(Ease.InQuad));

        disappearSequence.Append
            (_panelRectTransform.DOScale(0.75f, 0.125f)
            .From(1f)
            .SetEase(Ease.InQuad));

        disappearSequence.Join
            (_backgroundImage.DOFade(0f, 0.125f)
            .From(1f)
            .SetEase(Ease.InQuad));

        disappearSequence.Append
            (_fade.Disappear());

        disappearSequence.SetUpdate(true);
        disappearSequence.SetLink(gameObject);

        disappearSequence.OnKill(() =>
        {
            gameObject.SetActive(false);
        });

        return disappearSequence;
    }

    protected virtual void SubscribeOnEvents()
    {

    }

    protected virtual void UnsubscribeOnEvents()
    {

    }
}