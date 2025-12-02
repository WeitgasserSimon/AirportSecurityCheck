using UnityEngine;

public class LidController_Quaternion : MonoBehaviour
{
    public float maxOpenAngle = 120f;
    public float speed = 40f;
    public float closingSpeed = 30f;
    public float autoOpenSpeed = 20f;

    public float autoOpenThreshold = 100f;
    public KeyCode interactKey = KeyCode.E;

    private Quaternion closedRot;
    private Quaternion openRot;

    private enum LidState { Normal, AutoOpen, ForcedClosing }
    private LidState state = LidState.Normal;

    void Start()
    {
        closedRot = transform.localRotation;
        openRot = closedRot * Quaternion.Euler(0f, 0f, maxOpenAngle);
    }

    void Update()
    {
        float angle = Quaternion.Angle(closedRot, transform.localRotation);
        bool pressed = Input.GetKey(interactKey);

        Quaternion target = transform.localRotation;
        float step = 0f;

        switch (state)
        {
            // ---------------------------------------------------
            // NORMAL
            // ---------------------------------------------------
            case LidState.Normal:

                if (pressed)
                {
                    // E gedrückt => immer öffnen (Blockade weg!)
                    target = openRot;
                    step = speed * Time.deltaTime;

                    // WICHTIG:
                    // NICHT in AutoOpen wechseln, solange E gedrückt ist
                }
                else
                {
                    // E losgelassen => schließen
                    target = closedRot;
                    step = closingSpeed * Time.deltaTime;

                    // Übergang nur wenn NICHT gedrückt
                    if (angle > autoOpenThreshold)
                        state = LidState.AutoOpen;
                }

                break;

            // ---------------------------------------------------
            // AUTOOPEN (über 100°, E NICHT gedrückt)
            // ---------------------------------------------------
            case LidState.AutoOpen:

                if (pressed)
                {
                    // E wird gedrückt → Closing erzwingen
                    state = LidState.ForcedClosing;
                    break;
                }

                target = openRot;
                step = autoOpenSpeed * Time.deltaTime;
                break;

            // ---------------------------------------------------
            // FORCED CLOSING (über 100°, E gedrückt)
            // ---------------------------------------------------
            case LidState.ForcedClosing:

                target = closedRot;
                step = closingSpeed * Time.deltaTime;

                // Unter 100° → zurück zu Normal
                if (angle < autoOpenThreshold)
                    state = LidState.Normal;

                break;
        }

        transform.localRotation =
            Quaternion.RotateTowards(transform.localRotation, target, step);
    }
}
