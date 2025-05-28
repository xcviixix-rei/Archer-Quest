using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas canvas;

    void Awake()
    {
        canvas = FindFirstObjectByType<Canvas>();
    }
    private void OnEnable()
    {
        CharacterEvents.OnCharacterTookDamage += CharacterTookDamage;
        CharacterEvents.OnCharacterHealed += CharacterHealed;
    }
    private void OnDisable()
    {
        CharacterEvents.OnCharacterTookDamage -= CharacterTookDamage;
        CharacterEvents.OnCharacterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageAmount)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text damageText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        damageText.text = "-" + damageAmount.ToString();
    }

    public void CharacterHealed(GameObject character, int healAmount)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text healthText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        healthText.text = "+" + healAmount.ToString();
    }
}
