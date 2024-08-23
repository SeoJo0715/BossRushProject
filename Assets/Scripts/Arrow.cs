using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IProjectile
{
    [SerializeField] private Vector2 point;
    [SerializeField] private float duration;

    private bool isFiring;

    public void Firing()
    {
        if (!isFiring)
        {
            gameObject.SetActive(true);
            StartCoroutine(FiringArrow());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(FiringArrow());
        isFiring = false;
    }

    private IEnumerator FiringArrow()
    {
        isFiring = true;

        Vector3 p0 = transform.position;
        Vector3 p1 = transform.position + new Vector3(point.x / 2, point.y, 0);
        Vector3 p2 = transform.position + new Vector3(point.x, 0, 0);

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;

            Vector3 position = CalculateBezierPoint(t, p0, p1, p2);

            transform.position = position;

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        gameObject.SetActive(false);
        isFiring = false;
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        Vector3 p0p1 = Vector3.Lerp(p0, p1, t);
        Vector3 p1p2 = Vector3.Lerp(p1, p2, t);

        return Vector3.Lerp(p0p1, p1p2, t);
    }
}
