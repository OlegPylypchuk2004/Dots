using UnityEngine;

public class Panel : MonoBehaviour
{
    public void Appear()
    {
        gameObject.SetActive(true);
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
    }
}