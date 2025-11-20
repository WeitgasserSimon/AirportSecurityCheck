using UnityEngine;

/// <summary>
/// Script für den TMP Button: ruft MoveNextItem() im ConveyorController auf.
/// </summary>
public class StartButton : MonoBehaviour
{
    public Conveyor conveyor;

    // Diese Methode im Button OnClick() einstellen
    public void OnButtonClick()
    {
        if (conveyor == null)
        {
            Debug.LogWarning("StartButton: ConveyorController ist nicht gesetzt!");
            return;
        }

        conveyor.MoveNextItem();
        Debug.Log("StartButton: Button gedrückt -> MoveNextItem() aufgerufen");
    }
}
