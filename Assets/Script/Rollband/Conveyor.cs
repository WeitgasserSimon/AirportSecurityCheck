using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Steuert die sequentielle Bewegung von Items von pointA nach pointB.
/// Jedes Item wird auf pointA gesetzt, bewegt sich zu pointB, dann wird das n�chste Item aktiviert.
/// </summary>
public class Conveyor : MonoBehaviour
{
    [Header("Positions")]
    public Transform pointA;            // Startposition (z.B. links vom Band)
    public Transform pointB;            // Endposition (z.B. rechts vom Band)

    [Header("Conveyor Settings")]
    public float speed = 2f;            // Bewegungsgeschwindigkeit
    public List<GameObject> items;      // Liste der Items (in Inspector bef�llen)

    private int currentIndex = 0;       // Index des aktuellen Items in der Liste
    private bool isMoving = false;      // ob gerade ein Item bewegt wird

    void Start()
    {
        // Sicherheitscheck
        if (pointA == null || pointB == null)
            Debug.LogWarning("ConveyorController: pointA oder pointB ist nicht gesetzt!");
        if (items == null || items.Count == 0)
            Debug.LogWarning("ConveyorController: keine Items in der Liste!");
    }

    void Update()
    {
        if (!isMoving || currentIndex >= items.Count) return;

        GameObject currentItem = items[currentIndex];
        if (currentItem == null)
        {
            Debug.LogWarning("ConveyorController: current item is null -> skip");
            isMoving = false;
            currentIndex++;
            return;
        }

        // Bewegen (Position direkt setzen, MoveTowards)
        currentItem.transform.position = Vector3.MoveTowards(
            currentItem.transform.position,
            pointB.position,
            speed * Time.deltaTime
        );

        // wenn nahe genug an pointB
        if (Vector3.Distance(currentItem.transform.position, pointB.position) < 0.05f)
        {
            Debug.Log($"Conveyor: Item angekommen -> {currentItem.name}");
            isMoving = false;
            currentIndex++;
        }
    }

    /// <summary>
    /// Startet das n�chste Item. Wird vom Button oder extern aufgerufen.
    /// </summary>
    public void MoveNextItem()
    {
        if (currentIndex >= items.Count)
        {
            Debug.Log("ConveyorController: Keine Items mehr.");
            return;
        }

        GameObject next = items[currentIndex];
        if (next == null)
        {
            Debug.LogWarning("ConveyorController: next item null -> weiter");
            currentIndex++;
            return;
        }

        // Setze Item auf Startposition
        next.transform.position = pointA.position;

        // Optional: reset rotation/velocities falls ben�tigt
        Rigidbody rb = next.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        isMoving = true;
        Debug.Log($"ConveyorController: Starte Item {next.name}");
    }

    /// <summary>
    /// Hilfsfunktion: reset index (z.B. um Durchlauf neu zu starten)
    /// </summary>
    public void ResetConveyor(bool teleportToStart = true)
    {
        currentIndex = 0;
        isMoving = false;
        if (teleportToStart && items != null)
        {
            foreach (var it in items)
            {
                if (it != null && pointA != null)
                    it.transform.position = pointA.position;
            }
        }
        Debug.Log("ConveyorController: Reset durchgef�hrt.");
    }
}
