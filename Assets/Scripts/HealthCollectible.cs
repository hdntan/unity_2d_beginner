using UnityEngine;

public class HealthCollectible1 : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound;
    void OnTriggerEnter2D(Collider2D other)
    {
       
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && player.CurrentHealth < player.maxHealth)
        {
            player.PlaySound(collectSound); 
            player.ChangeHealth(1);
            Destroy(gameObject);
        }
    }
}
