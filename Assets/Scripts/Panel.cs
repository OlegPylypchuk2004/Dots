using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    [SerializeField] private Image _fadePanel;

    public void Appear()
    {
        gameObject.SetActive(true);
        _fadePanel.gameObject.SetActive(true);
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
        _fadePanel.gameObject.SetActive(false);
    }
}