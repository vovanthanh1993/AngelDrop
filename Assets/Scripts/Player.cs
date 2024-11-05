using UnityEngine;
using UnityEngine.UIElements;

public class Pot : MonoBehaviour
{
    public float moveSpeed = 1;
    float xDirection;

    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    float startY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 240;
        QualitySettings.vSyncCount = 0;
        mainCamera = Camera.main;
        startY = transform.position.y;
    }

    void Update()
    {
        if (GameController.Ins.isGameOver()) return;
        xDirection = Input.GetAxisRaw("Horizontal");
        float moveStep = moveSpeed * Time.deltaTime * xDirection;

        // Lấy vị trí của góc trên bên trái
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(new Vector3(20, Screen.height, 0));
        // Lấy vị trí của góc trên bên phải
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width - 20, Screen.height, 0));

        if (transform.position.x <= topLeft.x && xDirection == -1 || transform.position.x >= topRight.x && xDirection == 1) return;
        transform.position += new Vector3(moveStep, 0, 0);

        // Kiểm tra nếu có touch trên màn hình
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Bắt đầu kéo khi ngón tay chạm vào Sprite
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    offset = transform.position - touchPosition;
                }
            }
            // Di chuyển Sprite khi kéo ngón tay
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // Chuyển đổi vị trí chạm trên màn hình thành tọa độ thế giới
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                // Giữ nguyên vị trí Y, chỉ thay đổi vị trí X theo vị trí chạm
                transform.position = new Vector3(touchPosition.x, startY, 5);
            }
            // Dừng kéo khi ngón tay rời màn hình
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }
}
