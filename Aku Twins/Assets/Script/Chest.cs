using UnityEngine;
using UnityEngine.UI;


public class Chest : MonoBehaviour
{
    private Text interactUI;
    private bool isInRange;

    public int gemsToAdd;

    public AudioClip soundToPlay;

    public Animator animator;

    private void Awake()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        animator.SetTrigger("OpenChest");
        Inventory.instance.addGems(gemsToAdd);
        AudioManager.instance.PlayClipAt(soundToPlay, transform.position);
        GetComponent<BoxCollider2D>().enabled = false;
        interactUI.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = false;
            isInRange = false;
        }
    }
}
