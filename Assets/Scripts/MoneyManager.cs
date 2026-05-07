using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public int currentMoney = 0;
    public TextMeshProUGUI moneyText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        UpdateMoneyUI();
    }

    public void SetMoneyText(TextMeshProUGUI newMoneyText)
    {
        moneyText = newMoneyText;
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyUI();
    }

    public bool SpendMoney(int amount)
    {
        if (currentMoney < amount)
        {
            return false;
        }

        currentMoney -= amount;
        UpdateMoneyUI();
        return true;
    }

    public void LoseHalfMoney()
    {
        currentMoney = currentMoney / 2;
        UpdateMoneyUI();
    }

    public int GetMoney()
    {
        return currentMoney;
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = "x" + currentMoney.ToString();
        }
    }
}