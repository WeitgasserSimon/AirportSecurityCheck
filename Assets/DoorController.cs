using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float openAngle = 90f;      // Zielrotation in Grad
    public float closeAngle = 0f;      // Zurück zur Ausgangsrotation
    public float speed = 20f;          // Grad pro Sekunde

    [Header("Input Settings")]
    public KeyCode interactKey = KeyCode.E;

    private float targetAngle;         // Welcher Winkel aktuell angesteuert wird

    void Start()
    {
        // Tür startet geschlossen
        targetAngle = closeAngle;
    }

    void Update()
    {
        // E drücken zum Öffnen/Schließen
        if (Input.GetKeyDown(interactKey))
        {
            ToggleDoor();
        }

        // aktuelle Rotation holen (y-Achse)
        float currentAngle = transform.localEulerAngles.y;

        // Zielrotation zeitbasiert ansteuern
        float newAngle = Mathf.MoveTowards(
            currentAngle,
            targetAngle,
            speed * Time.deltaTime
        );

        // Rotation anwenden
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            newAngle,
            transform.localEulerAngles.z
        );
    }

    /// <summary>
    /// Öffnet, wenn geschlossen. Schließt, wenn offen.
    /// </summary>
    public void ToggleDoor()
    {
        if (Mathf.Approximately(targetAngle, closeAngle))
            targetAngle = openAngle;
        else
            targetAngle = closeAngle;
    }
}
