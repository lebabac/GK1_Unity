using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(PhaserWeapon.Instance.speed * Time.deltaTime, 0f);  
        if (transform.position.x > 11f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Khi va chạm với thiên thạch, chỉ destroy viên đạn
        if (collision.CompareTag("Obstacle"))
        {
            // gọi hàm TakeDamage() trong Asteroid
            Asteroid asteroid = collision.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                asteroid.TakeDamage(1); // giảm 1 máu
            }

            Destroy(gameObject); // chỉ destroy viên đạn
        }
    }
}
