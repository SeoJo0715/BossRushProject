using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float angle;

    private float angleValue;
    private bool isAttack;

    public void Attack()
    {
        if (!isAttack)
        {
            StartCoroutine(WieldSword());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(WieldSword());
        pivot.rotation = Quaternion.Euler(Vector3.zero);
        isAttack = false;
    }

    private IEnumerator WieldSword()
    {
        isAttack = true;

        float targetAngle = angle;
        int phase = 1;

        while (phase <= 3)
        {
            float currentAngle = NormalizeAngle(pivot.rotation.eulerAngles.z);

            pivot.rotation = Quaternion.RotateTowards(pivot.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed);

            if (Mathf.RoundToInt(currentAngle) == Mathf.RoundToInt(targetAngle))
            {
                phase++;
                switch (phase)
                {
                    case 2:
                        targetAngle = -angle;
                        break;
                    case 3:
                        targetAngle = 0;
                        break;
                }
            }

            yield return new WaitForSeconds(0.005f);
        }

        isAttack = false;
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
