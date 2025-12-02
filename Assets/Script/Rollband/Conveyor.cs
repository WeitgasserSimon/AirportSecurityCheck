using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Steuert die sequentielle Bewegung von Items von pointA nach pointB.
/// Die Reihenfolge der Items wird beim Start zufällig gemischt.
/// </summary>
public class Conveyor : MonoBehaviour
{
    [Header("Positions")]
    public Transform pointA;            // Startposition
    public Transform pointB;            // Endposition

    [Header("Conveyor Settings")]
    public float speed = 2f;            // Bewegungsgeschwindigkeit
    public List<GameObject> items;      // Liste der Items

    private int currentIndex = 0;       // Welches Item dran ist
    private bool isMoving = false;      // Ob gerade ein Item fährt

    void Start()
    {
        if (pointA == null || pointB == null)
            Debug.LogWarning("ConveyorController: pointA oder pointB ist nicht gesetzt!");
        if (items == null || items.Count == 0)
            Debug.LogWarning("ConveyorController: keine Items in der Liste!");

        ShuffleItems();   // <<< NEU: mischt die Liste einmal zufällig
    }

    /// <summary>
    /// Mischt die Itemliste mit Fisher–Yates Shuffle.
    /// </summary>
    private void ShuffleItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            int rand = Random.Range(i, items.Count);
            GameObject temp = items[i];
            items[i] = items[rand];
            items[rand] = temp;
        }

        Debug.Log("Conveyor: Items wurden zufällig gemischt.");
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

        currentItem.transform.position = Vector3.MoveTowards(
            currentItem.transform.position,
            pointB.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(currentItem.transform.position, pointB.position) < 0.05f)
        {
            Debug.Log($"Conveyor: Item angekommen -> {currentItem.name}");
            isMoving = false;
            currentIndex++;
        }
    }

    /// <summary>
    /// Startet das nächste Item (vom Button aufgerufen).
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

        next.transform.position = pointA.position;

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
    /// Reset funktioniert weiterhin wie vorher.
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

        Debug.Log("ConveyorController: Reset durchgeführt.");
    }
}
