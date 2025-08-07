using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rigidbody2d;
    [SerializeField] protected Animator animator;
    public float speed;
    public bool vertical;

    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    bool broken = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y += speed * direction * Time.fixedDeltaTime;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x += speed * direction * Time.fixedDeltaTime;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }

        

    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
       animator.SetTrigger("Fixed");
   }


}
