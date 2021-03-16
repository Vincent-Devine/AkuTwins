using UnityEngine;
using System.Linq;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        Inventory.instance.gemCount = PlayerPrefs.GetInt("gemCount", 0);
        Inventory.instance.UpdateTextUI();

        int currentHeath = PlayerPrefs.GetInt("playerHealth", PlayerHealth.instance.maxHealth);
        PlayerHealth.instance.currentHealth = currentHeath;
        PlayerHealth.instance.healthBar.setHealth(currentHeath);

        string[] itemsSaved = PlayerPrefs.GetString("inventoryItems", "").Split(',');
        for (int i = 0; i < itemsSaved.Length; i++)
        {
            if(itemsSaved[i] != "")
            {
                int id = int.Parse(itemsSaved[i]);
                Item currentItem = ItemsDatabase.instance.allItems.Single(x => x.id == id);
                Inventory.instance.content.Add(currentItem);
            }
        }

        Inventory.instance.UpdateInventoryUI();
    }

    public void SaveDate()
    {
        PlayerPrefs.SetInt("gemCount", Inventory.instance.gemCount);

        if(CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached",1))
        {
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
        }

        PlayerPrefs.SetInt("playerHealth", PlayerHealth.instance.currentHealth);

        string itemsInInventory = string.Join(",", Inventory.instance.content.Select(x => x.id));
        PlayerPrefs.SetString("inventoryItems",itemsInInventory);
    }
}
