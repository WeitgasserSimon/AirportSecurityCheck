using UnityEngine;

/// <summary>
/// Einfacher Trigger-Detector für XRay-Area.
/// Wenn ein Collider mit Tag "Illegal" oder "Legal" durchfährt, wird das geloggt.
/// </summary>
public class XRayDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Tags prüfen
        if (other.CompareTag("Illegal"))
        {
            Debug.Log($"XRayDetector: ⚠️ ILLEGALER Gegenstand entdeckt -> {other.name}");
            // Hier kannst du weitere Aktionen auslösen (z.B. Licht an, Alarm)
        }
        else if (other.CompareTag("Legal"))
        {
            Debug.Log($"XRayDetector: ✔️ LEGALER Gegenstand -> {other.name}");
        }
        else
        {
            Debug.Log($"XRayDetector: Unbekanntes Objekt ({other.tag}) -> {other.name}");
        }
    }
}
