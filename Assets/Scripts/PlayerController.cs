using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    [SerializeField] protected Rigidbody2D rigidbody2d;

    [SerializeField] protected Vector2 move;
    public float moveSpeed = 3.0f;

    public int maxHealth = 5;
    [SerializeField] protected int currentHealth;
    public float timeInvincible = 2.0f;
    [SerializeField]bool isInvincible;
    [SerializeField]float damageCooldown;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //         QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 10;
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)transform.position + move * moveSpeed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler1.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }


}

