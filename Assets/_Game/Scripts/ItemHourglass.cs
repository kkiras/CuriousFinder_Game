using UnityEngine;
using UnityEngine.InputSystem;

public class ItemHourglass : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Check click
        bool clicked =
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame);

        if (clicked)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(
                Mouse.current != null ?
                Mouse.current.position.ReadValue() :
                Touchscreen.current.primaryTouch.position.ReadValue()
            );

            // Raycast để bắt collider
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                GameManager.time += 10f;
            }
        }
    }
}
