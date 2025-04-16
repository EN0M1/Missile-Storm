using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerBehavior : MonoBehaviour
{
    private float timer = 0.0f;
    private TextMeshProUGUI m_text;
    private bool timerStarted = false;
    private string targetSceneName = "GamePlay";
    private float startTime;

    void Start()
    {
        m_text = GetComponent<TextMeshProUGUI>();

        if (m_text == null)
        {
            Debug.Log("No TextMeshProUGUI found");
        }

        timer = 0.0f;
    }

    void Update()
    {
        if (!timerStarted && SceneManager.GetActiveScene().name == targetSceneName)
        {
            startTime = Time.time;
            timerStarted = true;
        }

        if (timerStarted)
        {
            timer = Time.time - startTime;

            if (m_text != null)
            {
                int minutes = Mathf.FloorToInt(timer / 60);
                int seconds = Mathf.FloorToInt(timer % 60);
                string timeLabel = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
                m_text.SetText(timeLabel);
            }
        }
    }
}