using UnityEngine;

public class ColorChangeInArea : MonoBehaviour
{
    public Color targetColor = Color.red;
    [Range(0f, 1f)]
    public float targetAlpha = 0.5f;

    private Color originalColor;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Area"))
        {
            Color newColor = targetColor;
            newColor.a = targetAlpha;
            material.color = newColor;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Area"))
        {
            material.color = originalColor;
        }
    }
}
