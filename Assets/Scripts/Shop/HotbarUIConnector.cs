using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarUIConnector : MonoBehaviour
{
    public Image[] slotIcons;
    public TextMeshProUGUI[] slotAmounts;

    void Start()
    {
        if (HotbarManager.Instance != null)
        {
            HotbarManager.Instance.SetHotbarUI(slotIcons, slotAmounts);
        }
    }
}