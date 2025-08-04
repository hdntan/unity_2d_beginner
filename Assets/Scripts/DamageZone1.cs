using UnityEngine;

public class DamageZone1 : MonoBehaviour
{
     void OnTriggerStay2D(Collider2D other)
    {
       
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && player.CurrentHealth > 0)
        {
            player.ChangeHealth(-1);
            
        }
    }
}
