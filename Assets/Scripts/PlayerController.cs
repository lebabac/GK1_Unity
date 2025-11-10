using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 playerDirection;

    [SerializeField] private float moveSpeed;
    public float boost = 1f;
    public float boostPower = 5f;
    private bool boosting = false;

    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegen;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject destroyEffect;

    [Header("Fire Settings")]
    [SerializeField] private float fireRate = 0.2f; // thời gian giữa mỗi viên đạn
    private float fireTimer = 0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        energy = maxEnergy;
        UIController.Instance.UpdateEnergyUI(energy, maxEnergy);

        health = maxHealth;
        UIController.Instance.UpdateHealthUI(health, maxHealth);
    }

    void Update()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("moveX", directionX);
        animator.SetFloat("moveY", directionY);

        playerDirection = new Vector2(directionX, directionY).normalized;

        // Boost animation khi nhấn phím Space hoặc Fire2
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
        {
            EnterBoost();
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
        {
            ExitBoost();
        }

        // Bắn đạn liên tục khi giữ LeftShift hoặc Fire1
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Fire1"))
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                PhaserWeapon.Instance.Shoot();
                fireTimer = 0f;
            }
        }
        else
        {
            fireTimer = fireRate; // reset timer khi không giữ
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerDirection.x * moveSpeed, playerDirection.y * moveSpeed);
        // Quản lý năng lượng khi boosting
        if (boosting)
        {
            energy -= Time.fixedDeltaTime;
            if (energy <= 0)
            {
                energy = 0;
                ExitBoost();
            }
        }
        else
        {
            energy += energyRegen * Time.fixedDeltaTime;
            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
        }
        UIController.Instance.UpdateEnergyUI(energy, maxEnergy);
    }

    private void EnterBoost()
    {
        animator.SetBool("boosting", true);
        boost = boostPower;
        boosting = true;
    }

    private void ExitBoost()
    {
        animator.SetBool("boosting", false);
        boost = 1f;
        boosting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(10); // Giả sử mỗi lần va chạm với kẻ thù mất 10 máu
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        UIController.Instance.UpdateHealthUI(health, maxHealth);

        if (health <= 0)
        {
            boost = 0f;
            gameObject.SetActive(false);
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }
    }
}
