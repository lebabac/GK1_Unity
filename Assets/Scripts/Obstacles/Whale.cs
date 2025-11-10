using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Whale : MonoBehaviour
{
    [Header("Cấu hình Whale")]
    public int maxHealth = 5; // số viên đạn cần trúng
    private int currentHealth;

    public float moveSpeedMultiplier = 1f; // tốc độ di chuyển nhân với worldSpeed*boost

    [Header("Hiệu ứng trúng đạn")]
    [SerializeField] private Material hitMaterial; // nhấp nháy trắng
    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;

        currentHealth = maxHealth;
    }

    void Update()
    {
        // Di chuyển sang trái theo worldSpeed và boost
        float moveX = GameManager.Instance.worldSpeed * PlayerController.instance.boost * moveSpeedMultiplier * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0, 0);

        // Destroy khi ra khỏi màn hình
        if (transform.position.x < -11f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1); // trừ 1 máu
            Destroy(collision.gameObject); // hủy viên đạn
        }
    }

    // Hàm trừ máu và kiểm tra chết
    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        StartCoroutine(HitFlash());

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // cá voi chết
            // TODO: bạn có thể spawn explosion prefab ở đây
        }
    }

    // Nhấp nháy trắng khi trúng đạn
    private IEnumerator HitFlash()
    {
        spriteRenderer.material = hitMaterial;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = defaultMaterial;
    }
}
