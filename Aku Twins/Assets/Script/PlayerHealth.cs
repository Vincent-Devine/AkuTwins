using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isInvincible = false;
    public float invicibilityFlashDelay = 0.2f;
    public float invicibilityTimeAfterHit = 3f;


    public SpriteRenderer graphics;
    public HealthBar healthBar;

    public AudioClip hitSound;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(100);
        }
    }

    public void healPlayer(int amount)
    {
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        healthBar.setHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if(!isInvincible)
        {
            AudioManager.instance.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
            healthBar.setHealth(currentHealth);

            //Vérifier si le joueur est toujours vivant
            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
        }
    }

    public void Die()
    {
        Debug.Log("Le joueur est éliminé");
        //Bloqué les mouvements du joueur
        PlayerMovement.instance.enabled = false;
        //Jouer l'animation d'élimination
        PlayerMovement.instance.animator.SetTrigger("Die");
        //Enpêché les intérations physique avec les autres éléments de la scène
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.rb.velocity = Vector3.zero;
        PlayerMovement.instance.playerCollider.enabled = false;

        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        //Réactivé les mouvements du joueur
        PlayerMovement.instance.enabled = true;
        //Jouer l'animation de réactivation
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        //Réactivé les intérations physique avec les autres éléments de la scène
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.setHealth(currentHealth);
    }

    public IEnumerator InvicibilityFlash()
    {
        while(isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvincible = false;
    }
}
