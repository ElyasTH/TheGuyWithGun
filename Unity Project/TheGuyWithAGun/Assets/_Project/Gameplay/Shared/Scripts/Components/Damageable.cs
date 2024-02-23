using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damageable : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    
    [Header("Hit Settings")]
    [SerializeField] private bool addForceOnHit;
    
    private float _currentHealth;
    private bool _invincible;
    
    public void TakeDamage(float amount)
    {
        if (_invincible) return;
        
        _currentHealth -= amount;
        
        //Temp
        Flash();
        
        
        if (_currentHealth <= 0)
        {
            //Todo Die();
        }
    }
    
    public void Heal(float amount)
    {
        _currentHealth += amount;
        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }
    }

    public void SetInvincible()
    {
        _invincible = true;
    }
    
    public void SetVulnerable()
    {
        _invincible = false;
    }
    
    public bool hasForceOnHit()
    {
        return addForceOnHit;
    }
    
    public bool IsInvincible()
    {
        return _invincible;
    }
    
    //Temp FX
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }
    
    private void Flash()
    {
        _spriteRenderer.color = Color.white;
        Invoke(nameof(ResetColor), 0.1f);
    }
    
    private void ResetColor()
    {
        _spriteRenderer.color = _originalColor;
    }
    
    
}
