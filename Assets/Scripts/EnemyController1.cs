using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rigidbody2d;
    public float speed;
    public bool vertical;

    public float changeTime = 3.0f;
    float timer;
    int direction = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            position.y += speed * direction * Time.fixedDeltaTime;
        }
        else
        {
            position.x += speed * direction * Time.fixedDeltaTime;
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


}
