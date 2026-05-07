using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;

    private bool shopOpen;

    void Start()
    {
        CloseShop();
    }

    public void ToggleShop()
    {
        if (shopOpen)
        {
            CloseShop();
        }
        else
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
        }

        shopOpen = true;
        Time.timeScale = 0f;
    }

    public void CloseShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }

        shopOpen = false;
        Time.timeScale = 1f;
    }

    public void BuyItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogError("No item assigned to this shop button.");
            return;
        }

        if (MoneyManager.Instance == null)
        {
            Debug.LogError("MoneyManager.Instance is missing.");
            return;
        }

        if (HotbarManager.Instance == null)
        {
            Debug.LogError("HotbarManager.Instance is missing.");
            return;
        }

        Debug.Log("Trying to buy " + item.itemName + " for " + item.buyPrice);

        bool canPay = MoneyManager.Instance.SpendMoney(item.buyPrice);

        if (!canPay)
        {
            Debug.Log("Not enough money.");
            return;
        }

        bool added = HotbarManager.Instance.AddItem(item, 1);

        if (!added)
        {
            MoneyManager.Instance.AddMoney(item.buyPrice);
            Debug.Log("Hotbar is full. Refunding money.");
            return;
        }

        Debug.Log("Bought " + item.itemName);
    }
}