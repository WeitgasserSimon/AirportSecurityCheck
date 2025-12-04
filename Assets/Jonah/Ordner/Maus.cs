using UnityEngine;

public class Maus : MonoBehaviour
{
    public Projector projector;   // Dein Transparency Projector
    public float distance = 10f;  // Distanz vor der Kamera

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Projector Position auf den Hitpoint setzen
            projector.transform.position = hit.point + hit.normal * 0.1f;

            // Projector Richtung anpassen
            projector.transform.rotation = Quaternion.LookRotation(-hit.normal);
        }
    }
}
