using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    public Transform itemsContainer;
    public GameObject itemSlotPrefab;

    private Dictionary<HiddenItemData, GameObject> itemMap = new Dictionary<HiddenItemData, GameObject>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateShadowList();
    }

    void GenerateShadowList()
    {
        // Comment do GameManager không còn sử dụng 
        // foreach (var itemData in GameManager.Instance.itemsToFind)
        // {
        //     GameObject newSlot = Instantiate(itemSlotPrefab, itemsContainer);

        //     Image icon = newSlot.GetComponent<Image>();
        //     icon.sprite = itemData.shadowSprite;
        //     icon.preserveAspect = true;

        //     AspectRatioFitter fitter = newSlot.GetComponent<AspectRatioFitter>();
        //     if (fitter == null)
        //     {
        //         fitter = newSlot.AddComponent<AspectRatioFitter>();
        //     }
        //     fitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;

        //     itemMap.Add(itemData, newSlot);
        // }
    }

    public void RemoveItemFromUI(HiddenItemData data)
    {
        if (itemMap.ContainsKey(data))
        {
            Destroy(itemMap[data]);
            itemMap.Remove(data);
        }
    }

}
