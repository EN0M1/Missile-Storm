using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DashIconBehavior : MonoBehaviour
{
    TextMeshProUGUI label;
    Image overlay;
    float cooldown;
    float cooldownRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        Image[] images = GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].tag == "Overlay")
            {
                overlay = images[i];
            }
        }
        cooldownRate = PlaneBehavior.cooldownRate;
        overlay.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown = PlaneBehavior.cooldown;
        string message = "";
        if (cooldown > 0.0)
        {
            float fill = cooldown / cooldownRate;
            message = string.Format("{0:0.0}", cooldown);
            overlay.fillAmount = fill;
        }
        label.SetText(message);
    }
}
