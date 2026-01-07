using UnityEngine;
using UnityEngine.InputSystem;

public class ClickableItem : MonoBehaviour
{
    public HiddenItemData itemData;
    private bool isFound = false;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (isFound) return;

        // kiểm tra click
        bool clicked = (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
                       (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame);

        if (clicked)
        {
            Vector2 clickPos = mainCam.ScreenToWorldPoint(
                Mouse.current != null ? Mouse.current.position.ReadValue() :
                (Vector2)Touchscreen.current.primaryTouch.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                GameManager.Instance.OnItemClicked(this);
            }
        }
    }

    public void Collect()
    {
        isFound = true;
        gameObject.SetActive(false);
        // cập nhật thành animation "Sparkle" sau khi asset được cung cấp
    }
}
