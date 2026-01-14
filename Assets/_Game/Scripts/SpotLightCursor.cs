using UnityEngine;
using UnityEngine.InputSystem;

public class SpotLightCursor : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        worldPos.z = 0;

        transform.position = worldPos;
    }
}
