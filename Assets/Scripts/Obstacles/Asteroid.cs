using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Material whiteMaterial;
    private Material defaultMaterial;

    [SerializeField] private int health = 3; // trúng 3 viên mới nổ
    private bool isHit = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        defaultMaterial = spriteRenderer.material;
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1f);
        rb.linearVelocity = new Vector2(pushX, pushY).normalized;
    }

    void Update()
    {
        float moveX = (GameManager.Instance.worldSpeed * PlayerController.instance.boost) * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);

        if (transform.position.x < -11f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

     public void TakeDamage(int dmg)
    {
        if (isHit) return;
        isHit = true;
        StartCoroutine(ResetHit());

        health -= dmg;

        // hiệu ứng nhấp nháy trắng khi bị bắn
        spriteRenderer.material = whiteMaterial;
        StartCoroutine(ResetMaterial());

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = defaultMaterial;
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.1f);
        isHit = false;
    }
}
