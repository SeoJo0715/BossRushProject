using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    protected int maxHP;
    protected int currentHP;
    protected float attckCoolTime;

    protected void UpdateHPUI()
    {
        if(hpSlider != null)
        {
            hpSlider.value = (float)currentHP / maxHP;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(currentHP + " - " + damage);
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        
        UpdateHPUI();
    }
}
