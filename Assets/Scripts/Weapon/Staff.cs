using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float angle;
    [SerializeField] private SpriteRenderer crystal;
    [SerializeField] private Color glowColor;
    [SerializeField] private GameObject fireballPrefab;

    private float angleValue;
    private bool isAttack;
    private GameObject fireballObject;
    private Fireball fireball;

    public void Attack()
    {
        if (fireballObject == null)
        {
            fireballObject = Instantiate(fireballPrefab);
        }

        fireballObject.transform.position = transform.position;
        fireball = fireballObject.GetComponent<Fireball>();

        if (!isAttack)
        {
            StartCoroutine(WieldStaff());
            fireball.Firing();
        }
    }

    private void OnDisable()
    {
        StopCoroutine(WieldStaff());
        pivot.rotation = Quaternion.Euler(Vector3.zero);
        isAttack = false;
    }

    private IEnumerator WieldStaff()
    {
        isAttack = true;
        Color originalColor = crystal.color;
        float targetAngle = -angle;
        int phase = 1;
        crystal.color = glowColor;

        while (phase <= 2)
        {
            float currentAngle = NormalizeAngle(pivot.rotation.eulerAngles.z);

            pivot.rotation = Quaternion.RotateTowards(pivot.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed);

            if (Mathf.RoundToInt(currentAngle) == Mathf.RoundToInt(targetAngle))
            {
                phase++;

                if(phase == 2)
                {
                    targetAngle = 0;
                }
            }

            yield return new WaitForSeconds(0.005f);
        }

        isAttack = false;
        crystal.color = originalColor;
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
        {
            angle -= 360f;
        }

        return angle;
    }
}
