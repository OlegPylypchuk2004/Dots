using DG.Tweening;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] private PanelFade _fade;
    [SerializeField] private RectTransform _panelRectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;

    public virtual Sequence Appear()
    {
        gameObject.SetActive(true);
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0f;

        Sequence appearSequence = DOTween.Sequence();

        appearSequence.Join
            (_fade.Appear());

        appearSequence.Append
            (_panelRectTransform.DOScale(1f, 0.125f)
            .From(0.95f)
            .SetEase(Ease.OutQuad));

        appearSequence.Join
            (_canvasGroup.DOFade(1f, 0.125f)
            .From(0f)
            .SetEase(Ease.OutQuad));

        appearSequence.SetUpdate(true);
        appearSequence.SetLink(gameObject);

        appearSequence.OnKill(() =>
        {
            _canvasGroup.interactable = true;

            SubscribeOnEvents();
        });

        return appearSequence;
    }

    public virtual Sequence Disappear()
    {
        _canvasGroup.interactable = false;

        UnsubscribeOnEvents();

        Sequence disappearSequence = DOTween.Sequence();

        disappearSequence.Join
            (_panelRectTransform.DOScale(0.95f, 0.125f)
            .From(1f)
            .SetEase(Ease.InQuad));

        disappearSequence.Join
            (_canvasGroup.DOFade(0f, 0.125f)
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