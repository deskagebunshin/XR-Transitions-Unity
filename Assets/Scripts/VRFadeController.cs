using UnityEngine;

public class VRFadeController : MonoBehaviour
{
    [SerializeField] private Material fadeMaterial; // The material used for the fade quad
    [SerializeField] private float fadeDuration = 1.0f; // Duration of the fade
    public float FadeDuration => fadeDuration;
    [SerializeField] private string fadeShaderProperty = "_Fade"; // Name of the shader property controlling the fade
    private float currentAlpha = 0f; // Current alpha value
    private bool isFading = false;
    public static VRFadeController Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple VRFadeControllers detected in the scene. Only one will be used.");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // Ensure the material starts fully transparent
        SetFade(0f);
    }

    /// <summary>
    /// Fade out to black.
    /// </summary>
    public void FadeOut()
    {
        if (!isFading)
        {
            StartCoroutine(FadeTo(1f));
        }
    }

    /// <summary>
    /// Fade in to transparent.
    /// </summary>
    public void FadeIn()
    {
        if (!isFading)
        {
            StartCoroutine(FadeTo(0f));
        }
    }

    private System.Collections.IEnumerator FadeTo(float targetAlpha)
    {
        isFading = true;
        float startAlpha = currentAlpha;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            SetFade(currentAlpha);
            yield return null;
        }

        SetFade(targetAlpha);
        isFading = false;
    }

    /// <summary>
    /// Sets the alpha of the fade material.
    /// </summary>
    /// <param name="alpha">Alpha value (0 = fully transparent, 1 = fully opaque).</param>
    private void SetFade(float alpha)
    {
        fadeMaterial.SetFloat(fadeShaderProperty, alpha);

    }
}