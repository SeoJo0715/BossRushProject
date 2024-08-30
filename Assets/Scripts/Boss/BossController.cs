using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    protected int maxHP;
    protected float attckCoolTime;

    private int currentHP;

    private void Start()
    {
        currentHP = maxHP;
        UpdateHPUI();
    }

    private void UpdateHPUI()
    {
        if(hpSlider != null)
        {
            hpSlider.value = (float)currentHP / maxHP;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        Debug.Log(currentHP);
        UpdateHPUI();
    }
}
