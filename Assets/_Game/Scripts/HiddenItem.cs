using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HiddenItem : MonoBehaviour
{
    private bool isFound = false;
    private Camera mainCam;
    public GameObject relatedText;
    public GameObject relatedShadow;
    public Transform relatedTextPos;
    public Transform relatedShadowPos;
    private TextMesh textMesh;
    private SpriteRenderer shadowRenderer;


    void Start()
    {
        mainCam = Camera.main;
        if (relatedText != null)
            textMesh = relatedText.GetComponent<TextMesh>();
        if (relatedShadow != null)
            shadowRenderer = relatedShadow.GetComponent<SpriteRenderer>();
            
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
                if (textMesh != null &&
                    ColorUtility.TryParseHtmlString("#1A0B06", out Color color))
                {
                    color.a = 0.5f;
                    StopAllCoroutines(); 
                    StartCoroutine(FadeTextColor(color, 0.5f));
                }

                if (shadowRenderer != null)
                {
                    Color shadowTarget = shadowRenderer.color;
                    shadowTarget.a = 0.5f;
                    StopAllCoroutines(); 
                    StartCoroutine(FadeSpriteColor(shadowRenderer, shadowTarget, 0.5f));
                }

                // Xử lý bonus score
                if (UITrackingClick.timeBonusLimit > 0)
                {
                    UITrackingClick.rateBonusScore += 2;
                }
                UITrackingClick.timeBonusLimit = 5f;
            }
        }
    }

    // Text opacity transition
    IEnumerator FadeTextColor(Color targetColor, float duration)
    {
        Color startColor = textMesh.color;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            textMesh.color = Color.Lerp(
                startColor.linear,
                targetColor.linear,
                t / duration
            ).gamma;

            yield return null;
        }

        textMesh.color = targetColor;
    }

    // Shadow Opacity transition
    IEnumerator FadeSpriteColor(SpriteRenderer sr, Color targetColor, float duration)
    {
        if (sr == null) yield break;

        Color startColor = sr.color;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            sr.color = Color.Lerp(startColor, targetColor, t / duration);
            yield return null;
        }

        sr.color = targetColor;
    }

    public void Collect()
    {
        isFound = true;
        // gameObject.SetActive(false);
        // cập nhật thành animation "Sparkle" sau khi asset được cung cấp
    }
}
