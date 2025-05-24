using UnityEngine;

public class Damagable : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    private float _maxHealth = 100f;
    public float maxHealth
    {
        get { return _maxHealth; }
        private set { _maxHealth = value; }
    }
    public bool _isAlive = true;
    public bool isAlive
    {
        get { return _isAlive; }
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("Is Alive: " + value);
        }
    }
    [SerializeField]
    private float _currentHealth = 100f;
    private bool isInvincible = false;
    private float timeSinceLastHit = 0f;
    public float invincibilityTime = 0.25f;

    public float currentHealth
    {
        get { return _currentHealth; }
        private set
        {
            _currentHealth = Mathf.Clamp(value, 0, maxHealth);
            if (_currentHealth <= 0)
            {
                isAlive = false;
            }
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (isInvincible)
        {
            if (timeSinceLastHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceLastHit = 0f;
            }
            else
            {
                timeSinceLastHit += Time.deltaTime;
            }
        }
        Hit(5);
    }
    public void Hit(int damage)
    {
        if (isAlive && !isInvincible)
        {
            currentHealth -= damage;
            isInvincible = true;
        }
    }
}
