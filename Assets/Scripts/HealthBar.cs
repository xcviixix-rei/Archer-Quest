using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HealthSlider;
    Damageable damageable;
    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        damageable = player.GetComponent<Damageable>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HealthSlider.value = 1f;
    }

    private float calculateHealthPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
    private void onPlayerHealthChange(float currentHealth, float maxHealth)
    {
        HealthSlider.value = calculateHealthPercentage(currentHealth, maxHealth);
    }

    public void OnEnable()
    {
        damageable.healthChanged.AddListener(onPlayerHealthChange);
    }
    public void OnDisable()
    {
        damageable.healthChanged.RemoveListener(onPlayerHealthChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
