using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public Conveyor conveyor;

    public void OnButtonClick()
    {
        if (conveyor == null) return;
        conveyor.TogglePause();
    }
}
