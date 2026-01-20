using UnityEngine;
using System.Collections.Generic;

public class Conveyor : MonoBehaviour
{
    [Header("Positions")]
    public Transform pointA;
    public Transform pointB;

    [Header("Conveyor Settings")]
    public float speed = 1f;
    public float speedStep = 0.5f;
    public float minSpeed = 1f;
    public float maxSpeed = 10f;

    public List<GameObject> items; // Prefabs

    private GameObject currentItem;
    private bool isPaused = false;

    void Update()
    {
        // SPEED CONTROL
        if (Input.GetKeyDown(KeyCode.M))
        {
            speed = Mathf.Min(speed + speedStep, maxSpeed);
            Debug.Log("Conveyor Speed: " + speed);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            speed = Mathf.Max(speed - speedStep, minSpeed);
            Debug.Log("Conveyor Speed: " + speed);
        }

        if (currentItem == null) return;
        if (isPaused) return;

        currentItem.transform.position = Vector3.MoveTowards(
            currentItem.transform.position,
            pointB.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(currentItem.transform.position, pointB.position) < 0.05f)
        {
            Destroy(currentItem);
            currentItem = null;
        }
    }

    // BUTTON 1
    public void SpawnAndMove()
    {
        if (currentItem != null) return;
        if (isPaused) return;
        if (items == null || items.Count == 0) return;

        GameObject prefab = items[Random.Range(0, items.Count)];
        currentItem = Instantiate(prefab, pointA.position, Quaternion.identity);

        Rigidbody rb = currentItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log("Conveyor: Item gestartet");
    }

    // BUTTON 2
    public void TogglePause()
    {
        if (currentItem == null) return;

        isPaused = !isPaused;
        Debug.Log(isPaused ? "Conveyor: PAUSE" : "Conveyor: WEITER");
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public bool HasItem()
    {
        return currentItem != null;
    }
}
