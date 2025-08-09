using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public InputAction talkAction;
    [SerializeField] protected Rigidbody2D rigidbody2d;

    [SerializeField] protected Vector2 move;
    public float moveSpeed = 3.0f;

    public int maxHealth = 5;
    [SerializeField] protected int currentHealth;
    public float timeInvincible = 2.0f;
    [SerializeField] bool isInvincible;
    [SerializeField] float damageCooldown;
    Animator animator;
    [SerializeField] Vector2 moveDirection = new Vector2(1, 0);
    public GameObject projectilePrefab;
    // Khai báo biến AudioSource
    private AudioSource audioSource;

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
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        MoveAction.Enable();
        talkAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();

        }
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        Debug.Log(move);
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            FindFriend();
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
            animator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler1.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
    }

    void FindFriend()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            NonPlayerCharacter1 character = hit.collider.GetComponent<NonPlayerCharacter1>();
            if (character != null)
            {
                UIHandler1.instance.DisplayDialogue();
            }
        }

    }
    
       public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}

