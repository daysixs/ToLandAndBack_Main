using UnityEngine;

public class GlobalBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 stageDimensions;
    public int damage = 1;
    public float bulletSpeed = 10f;
    private Rigidbody2D rb;
    private Animator animator;
    public Vector3 offset;
    public bool isPlayerOwned;
    private bool breaking;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -stageDimensions.x * 2 || transform.position.x > stageDimensions.x * 2 || transform.position.y < -stageDimensions.y * 2 || transform.position.y > stageDimensions.y * 2)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D data)
    {
        Debug.Log("Hit!");
        if (isPlayerOwned)
        { }
        else
        {
            if (data.gameObject.tag == "Player")
            {
                HealthSystem target = data.GetComponent<HealthSystem>();
                if (target != null && breaking == false)
                {
                    rb.velocity = new Vector3(0f, 0f);
                    breaking = true;
                    target.TakeDamage(damage);
                    animator.SetBool("startDestroy", true);
                }
            }
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
