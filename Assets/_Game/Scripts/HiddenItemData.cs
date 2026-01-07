using System.Data.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Hidden Object/Item Data")]
public class HiddenItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite iconSprite;
    public Sprite shadowSprite;
}
