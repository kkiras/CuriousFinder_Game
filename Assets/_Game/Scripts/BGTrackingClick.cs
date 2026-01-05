using UnityEngine;
using UnityEngine.InputSystem;

public class BGTrackingClick : MonoBehaviour
{
    private Camera cam;
    public static int successClick = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Check click
        bool clicked =
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame);


        if (clicked)
        {
            successClick +=1;
            Debug.Log("Success click: " + successClick);

            if(UITrackingClick.timeBonusLimit > 0){
                UITrackingClick.rateBonusScore += 2;
            }

            UITrackingClick.timeBonusLimit = 5f;
        }
    }
}
