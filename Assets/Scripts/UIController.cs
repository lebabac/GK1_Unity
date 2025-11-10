using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    public GameObject pausePanel;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateEnergyUI(float currentEnergy, float maxEnergy)
    {
        energySlider.maxValue = maxEnergy;   // đảm bảo slider có đúng giới hạn
        energySlider.value = Mathf.RoundToInt(currentEnergy);
        energyText.text = energySlider.value + " / " + energySlider.maxValue;
    }

    public void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        healthSlider.maxValue = maxHealth;   // đảm bảo slider có đúng giới hạn
        healthSlider.value = Mathf.RoundToInt(currentHealth);
        healthText.text = healthSlider.value + " / " + healthSlider.maxValue;
    }
}
