using UnityEngine;
using UnityEngine.UI;

public class HammerCursor : MonoBehaviour
{
    public Image hammerCursorImage;        // Image UI hình búa
    public Sprite hammerIdleSprite;        // Sprite búa bình thường
    public Sprite hammerHitSprite;         // Sprite búa đập

    public float clickDuration = 0.2f;     // Thời gian búa đập hiển thị

    void Start()
    {
        // Ẩn con trỏ mặc định
        Cursor.visible = false;
    }

    void Update()
    {
        // Di chuyển con trỏ UI theo vị trí chuột
        Vector3 cursorPos = Input.mousePosition;
        cursorPos.x += 25f;
        cursorPos.y += 10f;
        hammerCursorImage.transform.position = cursorPos;

        // Nếu click chuột trái → đổi sprite trong thời gian ngắn
        if (Input.GetMouseButtonDown(0))
        {
            hammerCursorImage.sprite = hammerHitSprite;
            CancelInvoke(nameof(ResetCursor));  // Tránh delay chồng nhau
            Invoke(nameof(ResetCursor), clickDuration);
        }
    }

    void ResetCursor()
    {
        hammerCursorImage.sprite = hammerIdleSprite;
    }
}
