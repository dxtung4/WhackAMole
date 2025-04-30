using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Mole : MonoBehaviour, IPointerClickHandler
{
    // Các sprite tương ứng với các trạng thái
    public Sprite popupSprite;  // Sprite khi Mole ở trạng thái popup
    public Sprite warningSprite;  // Sprite khi Mole ở trạng thái Warning
    public Sprite hitSprite;  // Sprite khi Mole bị Hit

    private Image image;  // Đối tượng Image để thay đổi sprite
    private bool isHit = false;  // Biến kiểm tra xem Mole đã bị đánh hay chưa

void Start()
{
    image = GetComponent<Image>();

    if (image == null)
    {
        Debug.LogError("Không tìm thấy component Image trong Mole.");
    }

    if (popupSprite == null || warningSprite == null || hitSprite == null)
    {
        Debug.LogError("Một trong các sprite (popupSprite, warningSprite, hitSprite) chưa được gán.");
    }

    image.sprite = popupSprite;

    StartCoroutine(DelayedWarning());
    Destroy(gameObject, 2.5f);  // Tự huỷ sau 2.5s nếu không bị đập
}

    IEnumerator DelayedWarning()
    {
        yield return new WaitForSeconds(1.5f);  // Đợi đủ thời gian
        TriggerWarning();  // Sau đó mới gọi TriggerWarning
    }

public void TriggerWarning()
{
    if (!isHit && image != null && warningSprite != null)
    {
        image.sprite = warningSprite;  // Đổi sprite thành Warning
    }
    else
    {
        Debug.LogWarning("Không thể chuyển sprite vì thiếu Image hoặc Warning Sprite.");
    }
}



    // Phương thức khi Mole bị nhấn (Click)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isHit) return;  // Nếu đã bị đánh thì không làm gì thêm

        isHit = true;

        // Đổi sprite thành Hit khi bị nhấn
        image.sprite = hitSprite;

        // Tăng điểm cho game
        GameObject.Find("GameManager").GetComponent<GameManager>().AddScore(1);

        // Biến mất sau 0.3s
        Destroy(gameObject, 0.3f);
    }

    // Phương thức để khôi phục lại sprite thành Idle
    public void ResetToIdle()
    {
        image.sprite = popupSprite;  // Khôi phục lại sprite thành popup
    }
}
