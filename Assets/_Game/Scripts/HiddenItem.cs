using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HiddenItem : MonoBehaviour
{
    public HiddenItemData itemData;
    public GameObject relatedShadow;
    private bool isFound = false;
    private Camera mainCam;
    public Transform relatedTextPos;
    public Transform relatedShadowPos;
    private SpriteRenderer shadowRenderer;

    void Start()
    {
        mainCam = Camera.main;

        if (relatedShadow != null)
            shadowRenderer = relatedShadow.GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (isFound) return;

    }

    public void OnItemClicked()
    {
        if (isFound) return;
        isFound = true;

        //Có thể chuyển code này sang GameManager
        if (shadowRenderer != null)
        {
            Color shadowTarget = shadowRenderer.color;
            shadowTarget.a = 0.5f;
            StartCoroutine(FadeSpriteColor(shadowRenderer, shadowTarget, 0.5f));
        }
        //////////////////////////


        else
        {
            GameManager.Instance.OnItemCollected(itemData.id, this);
        }

        GetComponent<Collider2D>().enabled = false;
    }

    // private void ApplyFadeEffects()
    // {
    //     if (textMesh != null &&
    //         ColorUtility.TryParseHtmlString("#1A0B06", out Color color))
    //     {
    //         color.a = 0.5f;
    //         StopAllCoroutines(); 
    //         StartCoroutine(FadeTextColor(color, 0.5f));
    //     }

    //     if (shadowRenderer != null)
    //     {
    //         Color shadowTarget = shadowRenderer.color;
    //         shadowTarget.a = 0.5f;
    //         StopAllCoroutines(); 
    //         StartCoroutine(FadeSpriteColor(shadowRenderer, shadowTarget, 0.5f));
    //     }
    // }

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
