using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<HiddenItemData> itemsToFind = new List<HiddenItemData>();

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
    }

    public void OnItemClicked(ClickableItem itemClicked)
    {
        if (itemsToFind.Contains(itemClicked.itemData))
        {
            Debug.Log("Item found: " + itemClicked.itemData.displayName);

            itemClicked.Collect();
            itemsToFind.Remove(itemClicked.itemData);

            // Xử lý bonus score
            if (UITrackingClick.timeBonusLimit > 0)
            {
                UITrackingClick.rateBonusScore += 2;
            }
            UITrackingClick.timeBonusLimit = 5f;

            if (UIManager.Instance != null)
            {
                UIManager.Instance.RemoveItemFromUI(itemClicked.itemData);
            }
        }
        else
        {
            Debug.LogWarning("Item not in the list");
        }
    }
}
