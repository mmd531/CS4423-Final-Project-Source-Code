using UnityEngine;
using TMPro;

public class MoneyTextConnector : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    void Start()
    {
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.SetMoneyText(moneyText);
        }
    }
}
