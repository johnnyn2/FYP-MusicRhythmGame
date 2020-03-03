using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int cost;
    public bool isSold;
    public Color backgroundColor;
}
