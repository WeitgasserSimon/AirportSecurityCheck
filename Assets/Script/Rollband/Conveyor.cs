using UnityEngine;
using System.Collections.Generic;

public class Conveyor : MonoBehaviour
{
    [Header("Positions")]
    public Transform pointA;
    public Transform pointB;

    [Header("Conveyor Settings")]
    public float speed = 1f;

    [Tooltip("Liste von Prefabs, aus denen zufällig gewählt wird.")]
    public List<GameObject> items; // Prefabs!

    // Alle aktuell fahrenden Instanzen
    private List<GameObject> activeInstances = new List<GameObject>();


    void Update()
    {
        // Alle Instanzen unabhängig bewegen
        for (int i = activeInstances.Count - 1; i >= 0; i--)
        {
            GameObject inst = activeInstances[i];
            if (inst == null)
            {
                activeInstances.RemoveAt(i);
                continue;
            }

            inst.transform.position = Vector3.MoveTowards(
                inst.transform.position,
                pointB.position,
                speed * Time.deltaTime
            );

            // Wenn fertig -> löschen
            if (Vector3.Distance(inst.transform.position, pointB.position) < 0.05f)
            {
                Destroy(inst);
                activeInstances.RemoveAt(i);
            }
        }
    }


    /// <summary>
    /// Button ruft das auf -> spawnt 1 zufälliges Item und bewegt es.
    /// </summary>
    public void MoveNextItem()
    {
        if (items == null || items.Count == 0)
        {
            Debug.LogWarning("Conveyor: Keine Items in der Liste!");
            return;
        }

        // Zufälliges Item aus der Liste
        GameObject prefab = items[Random.Range(0, items.Count)];
        if (prefab == null)
        {
            Debug.LogWarning("Conveyor: null Prefab in der Liste!");
            return;
        }

        // Neue Instanz erstellen
        GameObject newItem = Instantiate(prefab, pointA.position, Quaternion.identity);

        // Physik reset
        Rigidbody rb = newItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        activeInstances.Add(newItem);

        Debug.Log($"Conveyor: Neues Item gestartet -> {newItem.name}");
    }


    /// <summary>
    /// Entfernt alle aktuell fahrenden Instanzen.
    /// </summary>
    public void ResetConveyor()
    {
        for (int i = 0; i < activeInstances.Count; i++)
        {
            if (activeInstances[i] != null)
                Destroy(activeInstances[i]);
        }

        activeInstances.Clear();
        Debug.Log("Conveyor: Reset durchgeführt.");
    }
}
