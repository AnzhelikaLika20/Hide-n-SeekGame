using System;
using System.Collections;
using UnityEngine;

public class VisibleTarget : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private float _fadeDuration = 1f;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        SetAlpha(0f);
    }

    public void OnVisible(float fadeDuration)
    {
        _fadeDuration = fadeDuration;
        StartFade(1f);
    }

    public void OnNotVisible()
    {
        StartFade(0f);
    }

    private void StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(targetAlpha));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = meshRenderer.material.color.a;
        for (float t = 0; t < _fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / _fadeDuration;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, normalizedTime);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(targetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color color = meshRenderer.material.color;
        color.a = alpha;
        meshRenderer.material.color = color;
        meshRenderer.enabled = alpha > 0;
    }
}