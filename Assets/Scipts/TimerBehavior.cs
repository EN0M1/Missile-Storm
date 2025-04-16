using TMPro;
using UnityEngine;

public class TimerBehavior : MonoBehaviour
{

    private float timer = 0.0f;
    private TextMeshProUGUI m_text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_text = GetComponent<TextMeshProUGUI>();
        Component[] cmps = GetComponents<Component>();

        if (m_text == null)
        {
            Debug.Log("No TextMeshProUGU found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time;

        //Debug.Log("time thus far: " + timer);

        if (m_text != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int second = Mathf.FloorToInt(timer % 60);
            string timeLabel = string.Format("Time: {0:00}:{1:00}", minutes, second);
            m_text.SetText(timeLabel);

        }
    }
}
