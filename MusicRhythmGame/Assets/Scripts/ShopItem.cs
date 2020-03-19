using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int cost;
    public Color backgroundColor;
    public int incHealth;
    public int damageRed;
    public string itemEffect;
    public bool isbrought;
}
