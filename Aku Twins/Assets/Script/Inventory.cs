using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int gemCount;
    public Text gemCountText;

    public List<Item> content = new List<Item>();
    private int contentCurrentIndex = 0;
    public Image itemImageUI;
    public Text itemNameUI;
    public Sprite emptyItemImage;

    public PlayerEffects playerEffects;

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        UpdateInventoryUI();
    }

    public void ConsumeItem()
    {
        if(content.Count == 0)
        {
            return;
        }

        Item currentItem = content[contentCurrentIndex];
        PlayerHealth.instance.healPlayer(currentItem.hpGiven);
        playerEffects.AddSpeed(currentItem.speedGiven, currentItem.speedDuration);
        content.Remove(currentItem);
        GetNextItem();
        UpdateInventoryUI();
    }

    public void GetNextItem()
    {
        if (content.Count == 0)
        {
            return;
        }

        contentCurrentIndex++;
        if(contentCurrentIndex > content.Count - 1)
        {
            contentCurrentIndex = 0;
        }
        UpdateInventoryUI();
    }

    public void GetPreviousItem()
    {
        if (content.Count == 0)
        {
            return;
        }

        contentCurrentIndex--;
        if (contentCurrentIndex < 0)
        {
            contentCurrentIndex = content.Count - 1;
        }
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        if (content.Count > 0)
        {
            itemImageUI.sprite = content[contentCurrentIndex].image;
            itemNameUI.text = content[contentCurrentIndex].name;
        }
        else
        {
            itemImageUI.sprite = emptyItemImage;
            itemNameUI.text = "";
        }

    }

    public void addGems(int count)
    {
        gemCount += count;
        UpdateTextUI();
    }

    public void RemoveGems(int count)
    {
        gemCount -= count;
        UpdateTextUI();
    }

    public void UpdateTextUI()
    {
        gemCountText.text = gemCount.ToString();
    }
}
