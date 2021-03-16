using UnityEngine;

public class PickUpGem : MonoBehaviour
{
    public AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(sound, transform.position) ;
            Inventory.instance.addGems(1);
            CurrentSceneManager.instance.gemsPickedUpInThisSceneCount++;
            Destroy(gameObject);
        }
    }
}
