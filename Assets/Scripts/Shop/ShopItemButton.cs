using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    public ItemData item;
    public TextMeshProUGUI priceText;
    public Image itemIcon;
    public ShopManager shopManager;

    void Start()
    {
        UpdateButtonUI();
    }

    void OnEnable()
    {
        UpdateButtonUI();
    }

    public void Buy()
    {
        Debug.Log("Buy button pressed.");

        if (shopManager != null)
        {
            shopManager.BuyItem(item);
        }
        else
        {
            Debug.LogError("ShopManager is not assigned on " + gameObject.name);
        }
    }

    void UpdateButtonUI()
    {
        if (item == null)
        {
            return;
        }

        if (priceText != null)
        {
            priceText.text = "x" + item.buyPrice.ToString();
        }

        if (itemIcon != null)
        {
            itemIcon.sprite = item.icon;
            itemIcon.color = Color.white;
            itemIcon.enabled = item.icon != null;
        }
    }
}