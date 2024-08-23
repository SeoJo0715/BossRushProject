using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject arrowPrefab;

    private GameObject arrowObject;
    private Arrow arrow;

    public void Attack()
    {
        if(arrowObject == null)
        {
            arrowObject = Instantiate(arrowPrefab);
        }
        
        arrowObject.transform.position = transform.position;
        arrow = arrowObject.GetComponent<Arrow>();

        arrow.Firing();
    }
}
