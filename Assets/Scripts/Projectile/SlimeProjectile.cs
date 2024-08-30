using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float point;
    [SerializeField] private float duration;

    private bool isFiring;

    public void Firing()
    {
        if (!isFiring)
        {
            gameObject.SetActive(true);
            StartCoroutine(FiringProjectile());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(FiringProjectile());
        isFiring = false;
    }

    private IEnumerator FiringProjectile()
    {
        isFiring = true;
        Vector3 startPoint = new Vector3(2.45f, 0, 0);
        Vector3 endPoint = startPoint - new Vector3(point, 0, 0);

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
