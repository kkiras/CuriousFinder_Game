using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // public List<HiddenItemData> itemsToFind = new List<HiddenItemData>();
    public static float time = 60f;
    public Transform timer;
    // public static int itemsToFind;
    [System.Serializable]
    public class ItemUIBinding {
        public HiddenItemData data;
        public TextMesh textMesh;
        // Có thể thêm shadow ở đây
        // public SpriteRenderer shadowRenderer;
    }
    public List<ItemUIBinding> uiBindings;
    private Dictionary<string, int> itemsRemaining = new Dictionary<string, int>();
    private Dictionary<string, TextMesh> uiMap = new Dictionary<string, TextMesh>();
    private Dictionary<string, string> nameMap = new Dictionary<string, string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (var binding in uiBindings) {
            if (binding.data != null) {
                string id = binding.data.id;
                uiMap.Add(id, binding.textMesh);
                nameMap.Add(id, binding.data.displayName);
                
                itemsRemaining.Add(id, binding.data.totalRequired);
                
                UpdateItemUI(id);
            }
        }
    }
    
    // Tạm thời comment do dùng logic ở HiddenItem.cs
    // public void OnItemClicked(ClickableItem itemClicked)
    // {
    //     if (itemsToFind.Contains(itemClicked.itemData))
    //     {
    //         Debug.Log("Item found: " + itemClicked.itemData.displayName);

    //         itemClicked.Collect();
    //         itemsToFind.Remove(itemClicked.itemData);

    //         // Xử lý bonus score
    //         if (UITrackingClick.timeBonusLimit > 0)
    //         {
    //             UITrackingClick.rateBonusScore += 2;
    //         }
    //         UITrackingClick.timeBonusLimit = 5f;

    //         if (UIManager.Instance != null)
    //         {
    //             UIManager.Instance.RemoveItemFromUI(itemClicked.itemData);
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Item not in the list");
    //     }
    // }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            timer.GetComponent<TextMesh>().text = time.ToString("F2");

        }
        HandleSelection();
    }

    private void HandleSelection()
    {
        bool clicked = (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
                       (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame);

        if (clicked)
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(
                Mouse.current != null ? Mouse.current.position.ReadValue() :
                (Vector2)Touchscreen.current.primaryTouch.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

            if (hit.collider != null)
            {
                HiddenItem item = hit.collider.GetComponent<HiddenItem>();
                if (item != null)
                {
                    item.OnItemClicked();
                }
            }
        }
    }

    public void OnItemCollected(string itemID, HiddenItem itemRef)
    {
        if (itemsRemaining.ContainsKey(itemID)) {
            itemsRemaining[itemID]--;
            UpdateItemUI(itemID);

            if (itemsRemaining[itemID] <= 0) {
                Debug.Log("Đã tìm hết loại: " + itemID);
                
                if (uiMap.ContainsKey(itemID)) {
                    StartCoroutine(FadeUIText(uiMap[itemID], 0.5f));
                }
            }
        }
        // Xử lý bonus score
        if (UITrackingClick.timeBonusLimit > 0)
        {
            UITrackingClick.rateBonusScore += 2;
        }
        UITrackingClick.timeBonusLimit = 5f;
        // itemsToFind -= 1;
    }

    IEnumerator FadeUIText(TextMesh textMesh, float duration)
    {
        if (textMesh == null) yield break;
        
        ColorUtility.TryParseHtmlString("#1A0B06", out Color targetColor);
        targetColor.a = 0.5f;

        Color startColor = targetColor;
        startColor.a = 1f;

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            textMesh.color = Color.Lerp(startColor.linear, targetColor.linear, t / duration).gamma;
            yield return null;
        }
        textMesh.color = targetColor;
    }

    private void UpdateItemUI(string itemID) {
        if (uiMap.ContainsKey(itemID) && uiMap[itemID] != null) {
            int count = itemsRemaining[itemID];
            string dName = nameMap[itemID];

            if (count > 1) 
                uiMap[itemID].text = $"{dName} x{count}";
            else 
                uiMap[itemID].text = dName;

        }
    }
}
