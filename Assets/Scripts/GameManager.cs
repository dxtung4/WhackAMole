using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject molePrefab;                   // Prefab chuột
    public Transform[] spawnPoints;                 // Các vị trí để chuột xuất hiện
    public float spawnInterval = 1.5f;              // Thời gian giữa các lần spawn
    public float gameDuration = 30f;                // Thời gian chơi game

    public Text scoreText;
    public Text timerText;

    private int score = 0;
    private float timer;

    void Start()
    {
        timer = gameDuration;
        StartCoroutine(SpawnMoles());
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            StopAllCoroutines(); // Hết giờ thì dừng game
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        timerText.text = "Time: " + Mathf.Ceil(timer);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    // Coroutine để sinh ra Mole
IEnumerator SpawnMoles()
{
    while (timer > 0)
    {
        int rand = Random.Range(0, spawnPoints.Length);
        Transform point = spawnPoints[rand];

        GameObject mole = Instantiate(molePrefab, point.position, Quaternion.identity);
        mole.transform.SetParent(point); // Gắn vào điểm spawn

        // Kiểm tra xem moleScript có phải là null không
        Mole moleScript = mole.GetComponent<Mole>();
        if (moleScript != null)
        {
            moleScript.TriggerWarning();  // Gọi phương thức để Mole chuyển sang trạng thái Warning
        }
        else
        {
            Debug.LogError("Không tìm thấy script Mole trong prefab molePrefab.");
        }

        yield return new WaitForSeconds(spawnInterval);
    }
}


    // Phương thức để gọi khi Mole bị đánh
    public void OnMoleHit(Mole mole)
    {
        mole.OnPointerClick(null);  // Giả lập việc nhấn vào Mole để đổi sprite sang Hit
        AddScore(1);  // Tăng điểm khi Mole bị đánh
    }
}
