using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : BossController
{
    [SerializeField] private GameObject projectilePrefab;

    private GameObject projectileObject;
    private SlimeProjectile slimeProjectile;

    private void Awake()
    {
        currentHP = 100;
        maxHP = 100;
        attckCoolTime = 0.5f;

        UpdateHPUI();
    }

    private void Start()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(20);

        if (projectileObject == null)
        {
            projectileObject = Instantiate(projectilePrefab);
        }

        projectileObject.transform.position = transform.position;
        slimeProjectile = projectileObject.GetComponent<SlimeProjectile>();

        for (int i = 0; i < 10; i++)
        {
            slimeProjectile.Firing();

            yield return new WaitForSeconds(attckCoolTime);
        }
    }
}
