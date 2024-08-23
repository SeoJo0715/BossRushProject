using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour, IProjectile
{
    [SerializeField] private float point;
    [SerializeField] private float duration;

    private bool isFiring;

    public void Firing()
    {
        if (!isFiring)
        {
            gameObject.SetActive(true);
            StartCoroutine(FiringFireball());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(FiringFireball());
        isFiring = false;
    }

    private IEnumerator FiringFireball()
    {
        isFiring = true;
        Vector3 startPoint = transform.position;
        Vector3 endPoint = transform.position + new Vector3(point, 0, 0);

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;

            transform.position = Vector3.Lerp(startPoint, endPoint, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        gameObject.SetActive(false);
        isFiring = false;
    }
}
