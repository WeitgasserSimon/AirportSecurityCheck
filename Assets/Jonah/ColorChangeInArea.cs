using UnityEngine;

public class ColorChangeInArea : MonoBehaviour
{
    [Range(0f, 1f)]
    public float transparentAlpha = 0.5f;

    private Material material;
    private Color originalColor;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Area"))
            return;

        if (CompareTag("Illegal"))
        {
            SetOpaque();
            material.color = new Color(1f, 0f, 0f, 1f); // Rot, nicht transparent
        }
        else if (CompareTag("Legal"))
        {
            SetTransparent();
            material.color = new Color(0f, 1f, 0f, transparentAlpha); // Grün, transparent
        }
        else if (CompareTag("Halblegal"))
        {
            SetTransparent();
            material.color = new Color(1f, 1f, 0f, transparentAlpha); // Gelb, transparent
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Area"))
            return;

        material.color = originalColor;
        SetOpaque();
    }

    // 🔴 OPAK
    void SetOpaque()
    {
        material.SetOverrideTag("RenderType", "");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHABLEND_ON");
        material.renderQueue = -1;
    }

    // 🟢 TRANSPARENT
    void SetTransparent()
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.EnableKeyword("_ALPHABLEND_ON");
        material.renderQueue = 3000;
    }
}
