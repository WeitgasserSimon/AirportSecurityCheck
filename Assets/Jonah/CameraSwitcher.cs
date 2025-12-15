using UnityEngine;


public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;


    void Start()
    {
        // Start mit Kamera 1
        camera1.enabled = true;
        camera2.enabled = false;
    }


    void Update()
    {
        // Taste B zum Wechseln
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchCamera();
        }
    }


    void SwitchCamera()
    {
        camera1.enabled = !camera1.enabled;
        camera2.enabled = !camera2.enabled;
    }
}