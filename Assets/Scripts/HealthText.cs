using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;
    public float fadeOutTime = 0f;
    public float destroyAfterTime = 1f;
    public float startAplha;

    void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startAplha = textMeshPro.alpha;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        fadeOutTime += Time.deltaTime;
        if (fadeOutTime < destroyAfterTime)
        {
            textMeshPro.alpha = startAplha * (1 - (fadeOutTime / destroyAfterTime));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
