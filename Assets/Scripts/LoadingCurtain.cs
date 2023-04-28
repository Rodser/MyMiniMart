using System.Collections;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    private CanvasGroup _curtain;

    private void Awake()
    {
        _curtain = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(this);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeOut());
    }

    public void Hide()
    {
        StartCoroutine(FadeIn());
        gameObject.SetActive(false);
    }

    private IEnumerator FadeIn()
    {
        {
            if (_curtain.alpha > 0)
            {
                _curtain.alpha -= 0.05f;
                yield return new WaitForSeconds(0.03f);
            }
        }
    }
        
    private IEnumerator FadeOut()
    {
        {
            if (_curtain.alpha < 1)
            {
                _curtain.alpha += 0.05f;
                yield return new WaitForSeconds(0.03f);
            }
        }
    }
}