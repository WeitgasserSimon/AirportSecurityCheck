using UnityEngine;

public class AutoTransparency : MonoBehaviour
{
    public Material targetMaterial;
    public KeyCode transparentKey = KeyCode.T;
    public KeyCode normalKey = KeyCode.A;

    public float fadeDuration = 1.5f;

    // Airport-Scanner Alpha
    public float targetAlpha = 0.03f;

    private bool isFading = false;

    private Color originalColor;   // normale Farbe speichern

    void Start()
    {
        originalColor = targetMaterial.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(transparentKey) && !isFading)
        {
            StartCoroutine(FadeToTransparent());
        }

        if (Input.GetKeyDown(normalKey) && !isFading)
        {
            StartCoroutine(FadeToOpaque());
        }
    }

    void OnApplicationQuit()
    {
        SetOpaqueMode();
        targetMaterial.color = originalColor;
    }

    System.Collections.IEnumerator FadeToTransparent()
    {
        isFading = true;

        SetTransparentMode();

        Color startColor = targetMaterial.color;
        float startAlpha = startColor.a;
        float time = 0f;

        // Airport Scanner Farbe (leichtes Blau)
        Color scannerColor = new Color(0.25f, 0.55f, 1f, startAlpha);

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;

            Color newColor = Color.Lerp(startColor, scannerColor, t);
            newColor.a = Mathf.Lerp(startAlpha, targetAlpha, t);

            targetMaterial.color = newColor;

            time += Time.deltaTime;
            yield return null;
        }

        // Endfarbe setzen
        Color finalScanColor = scannerColor;
        finalScanColor.a = targetAlpha;
        targetMaterial.color = finalScanColor;

        isFading = false;
    }

    System.Collections.IEnumerator FadeToOpaque()
    {
        isFading = true;

        Color startColor = targetMaterial.color;
        float startAlpha = startColor.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;

            Color newColor = Color.Lerp(startColor, originalColor, t);
            newColor.a = Mathf.Lerp(startAlpha, originalColor.a, t);

            targetMaterial.color = newColor;

            time += Time.deltaTime;
            yield return null;
        }

        targetMaterial.color = originalColor;

        SetOpaqueMode();

        isFading = false;
    }

    void SetTransparentMode()
    {
        targetMaterial.SetFloat("_Mode", 3);
        targetMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        targetMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        targetMaterial.SetInt("_ZWrite", 0);
        targetMaterial.DisableKeyword("_ALPHATEST_ON");
        targetMaterial.EnableKeyword("_ALPHABLEND_ON");
        targetMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        targetMaterial.renderQueue = 3000;
    }

    void SetOpaqueMode()
    {
        targetMaterial.SetFloat("_Mode", 0);
        targetMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        targetMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        targetMaterial.SetInt("_ZWrite", 1);
        targetMaterial.DisableKeyword("_ALPHATEST_ON");
        targetMaterial.DisableKeyword("_ALPHABLEND_ON");
        targetMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        targetMaterial.renderQueue = -1;
    }
}
