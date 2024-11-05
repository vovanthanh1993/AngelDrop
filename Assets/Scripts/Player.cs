using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1;
    float xDirection;
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

        // Dieu khien bang ban phim
        xDirection = Input.GetAxisRaw("Horizontal");
        float moveStep = moveSpeed * Time.deltaTime * xDirection;

        // Lấy vị trí của góc trên bên trái
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(new Vector3(20, Screen.height, 0));
        // Lấy vị trí của góc trên bên phải
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width - 20, Screen.height, 0));

        // Gioi han khi di chuyen bang ban phim
        if (transform.position.x <= topLeft.x && xDirection == -1 || transform.position.x >= topRight.x && xDirection == 1) return;
        transform.position += new Vector3(moveStep, 0, 0);

        // Di chuyen tren thiet bi di dong
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Chuyển đổi vị trí chạm trên màn hình thành tọa độ thế giới
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // Giữ nguyên vị trí Y, chỉ thay đổi vị trí X theo vị trí chạm
            transform.position = new Vector3(touchPosition.x, startY, 5);
        }
    }
}
