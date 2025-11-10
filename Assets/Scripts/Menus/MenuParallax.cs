using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private float backgroundWidth;

    private void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    private void Update()
    {
        float moveX = moveSpeed * Time.deltaTime * -1;

        transform.position += new Vector3(moveX, 0);

        // Khi background di chuyển vượt quá chiều rộng, reset lại
        if (transform.position.x <= -backgroundWidth)
        {
            transform.position = new Vector3(transform.position.x + backgroundWidth * 2f, transform.position.y, transform.position.z);
        }
    }
}
