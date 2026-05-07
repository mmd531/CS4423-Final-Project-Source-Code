using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager Instance;

    public HotbarSlot[] slots = new HotbarSlot[4];
    public Image[] slotIcons;
    public TextMeshProUGUI[] slotAmounts;

    private PlayerHP playerHP;
    private PlayerCombat playerCombat;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = new HotbarSlot();
            }
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void Start()
    {
        FindPlayerReferences();
        UpdateHotbarUI();
        UpdatePassiveEffects();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayerReferences();
        UpdateHotbarUI();
        UpdatePassiveEffects();
    }

    void FindPlayerReferences()
    {
        playerHP = FindFirstObjectByType<PlayerHP>();
        playerCombat = FindFirstObjectByType<PlayerCombat>();
    }

    public void SetHotbarUI(Image[] newSlotIcons, TextMeshProUGUI[] newSlotAmounts)
    {
        slotIcons = newSlotIcons;
        slotAmounts = newSlotAmounts;
        UpdateHotbarUI();
        UpdatePassiveEffects();
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            UseSlot(0);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            UseSlot(1);
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            UseSlot(2);
        }

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            UseSlot(3);
        }
    }

    public bool AddItem(ItemData itemData, int amount = 1)
    {
        if (itemData == null)
        {
            return false;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == itemData && slots[i].amount < itemData.maxStack)
            {
                int spaceLeft = itemData.maxStack - slots[i].amount;
                int amountToAdd = Mathf.Min(spaceLeft, amount);

                slots[i].amount += amountToAdd;
                amount -= amountToAdd;

                if (amount <= 0)
                {
                    UpdateHotbarUI();
                    UpdatePassiveEffects();
                    return true;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty())
            {
                slots[i].item = itemData;
                slots[i].amount = amount;

                UpdateHotbarUI();
                UpdatePassiveEffects();
                return true;
            }
        }

        return false;
    }

    public void UseSlot(int index)
    {
        if (index < 0 || index >= slots.Length)
        {
            return;
        }

        if (slots[index].IsEmpty())
        {
            return;
        }

        ItemData item = slots[index].item;

        if (item.itemType == ItemType.Consumable)
        {
            if (playerHP == null)
            {
                playerHP = FindFirstObjectByType<PlayerHP>();
            }

            if (item.healPercent > 0f && playerHP != null)
            {
                int healAmount = Mathf.RoundToInt(playerHP.GetMaxHealth() * item.healPercent);
                playerHP.Heal(healAmount);
            }

            RemoveOneFromSlot(index);
        }

        if (item.itemType == ItemType.Charm)
        {
            Debug.Log("Charm is passive. It works just by being in the hotbar.");
        }
    }

    void RemoveOneFromSlot(int index)
    {
        slots[index].amount--;

        if (slots[index].amount <= 0)
        {
            slots[index].Clear();
        }

        UpdateHotbarUI();
        UpdatePassiveEffects();
    }

    void UpdatePassiveEffects()
    {
        if (playerCombat == null)
        {
            playerCombat = FindFirstObjectByType<PlayerCombat>();
        }

        if (playerCombat == null)
        {
            return;
        }

        float attackMultiplier = 1f;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.itemType == ItemType.Charm)
            {
                attackMultiplier += slots[i].item.attackBoostPercent * slots[i].amount;
            }
        }

        playerCombat.SetAttackMultiplier(attackMultiplier);
    }

    public void UpdateHotbarUI()
    {
        if (slotIcons == null || slotAmounts == null)
        {
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (i >= slotIcons.Length || i >= slotAmounts.Length)
            {
                return;
            }

            if (slotIcons[i] == null || slotAmounts[i] == null)
            {
                return;
            }

            if (slots[i].item != null)
            {
                slotIcons[i].sprite = slots[i].item.icon;
                slotIcons[i].enabled = true;
                slotIcons[i].color = Color.white;
                slotAmounts[i].text = slots[i].amount.ToString();
            }
            else
            {
                slotIcons[i].sprite = null;
                slotIcons[i].enabled = false;
                slotAmounts[i].text = "";
            }
        }
    }
}