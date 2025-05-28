using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    Animator animator;
    public UnityEvent<int, Vector2> damageableHit;
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
    public bool lockVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set { animator.SetBool(AnimationStrings.lockVelocity, value); }
    }
    private bool isInvincible = false;
    private float timeSinceLastHit = 0f;
    public float invincibilityTime = 0.25f;

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
    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if (isAlive && !isInvincible)
        {
            currentHealth -= damage;
            isInvincible = true;
            lockVelocity = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.OnCharacterTookDamage?.Invoke(gameObject, damage);
            return true;
        }
        return false;
    }
    public bool Heal(int healAmount)
    {
        if (isAlive && currentHealth < maxHealth)
        {
            float tmpHealth = currentHealth;
            currentHealth += healAmount;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            healAmount = Mathf.RoundToInt(currentHealth - tmpHealth);
            CharacterEvents.OnCharacterHealed?.Invoke(gameObject, healAmount);
            return true;
        }
        return false;
    }
}
