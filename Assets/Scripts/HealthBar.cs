using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI")]
    public Slider slider;

    private int maxHealth;

    public void Init(int max)
    {
        maxHealth = max;

        if (slider == null)
        {
            Debug.LogError("HealthBar: Slider is NULL!");
            return;
        }

        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(int current)
    {
        if (slider == null)
        {
            Debug.LogError("HealthBar: Slider is NULL!");
            return;
        }

        slider.value = current;
    }
}
