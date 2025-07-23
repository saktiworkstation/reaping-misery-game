using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public int maxHealth = 100;
    public int health;
    public int mana = 50;
    public int strength = 10;

    void Awake()
    {
        health = maxHealth;
    }

    public void ModifyHealth(int amt)
    {
        health = Mathf.Clamp(health + amt, 0, maxHealth);
        Debug.Log($"Health ? {health}/{maxHealth}");
    }
    public void ModifyMana(int amt)
    {
        mana = Mathf.Max(mana + amt, 0);
        Debug.Log($"Mana ? {mana}");
    }
    public void ModifyStrength(int amt)
    {
        strength += amt;
        Debug.Log($"Strength ? {strength}");
    }
}
