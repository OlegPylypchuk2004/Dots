using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PanelFade : MonoBehaviour
{
    [SerializeField] private Image _image;

    public Tween Appear()
    {
        gameObject.SetActive(true);

        return _image.DOFade(0.95f, 0.25f)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject);
    }

    public Tween Disappear()
    {
        return _image.DOFade(0.9f, 0.25f)
            .SetEase(Ease.InQuad)
            .SetLink(gameObject)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}