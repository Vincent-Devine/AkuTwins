using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text pnjNameText;

    public Animator animator;

    public GameObject sellButtonPrefab;
    public Transform sellButtonsParent;

    public static ShopManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de ShopManager dans la scène");
            return;
        }

        instance = this;
    }

    public void OpenShop(Item[] items, string pnjName)
    {
        pnjNameText.text = pnjName;
        UpdateItemsToSell(items);
        animator.SetBool("isOpen", true);
    }

    public void UpdateItemsToSell(Item[] items)
    {
        for (int i = 0; i < sellButtonsParent.childCount; i++)
        {
            Destroy(sellButtonsParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < items.Length; i++)
        {
            GameObject button = Instantiate(sellButtonPrefab, sellButtonsParent);
            SellButtonItem buttonScript = button.GetComponent<SellButtonItem>();
            buttonScript.itemName.text = items[i].name;
            buttonScript.itemImage.sprite = items[i].image;
            buttonScript.itemPrice.text = items[i].price.ToString();

            buttonScript.item = items[i];

            button.GetComponent<Button>().onClick.AddListener(delegate { buttonScript.BuyItem(); } );
        }
    }

    public void closeShop()
    {
        animator.SetBool("isOpen", false);
    }
}
