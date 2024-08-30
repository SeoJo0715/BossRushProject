using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpDuration;

    private Vector2 moveInput;
    private Vector2 position;
    private float jumpTime;
    private bool isJumping;

    private IWeapon currentWeapon;
    private List<IWeapon> weaponComponents = new();
    private List<GameObject> weaponObjects = new();

    private void Start()
    {
        var sword = GetComponentInChildren<Sword>(true);
        var bow = GetComponentInChildren<Bow>(true);
        var staff = GetComponentInChildren<Staff>(true);

        weaponComponents.Add(sword);
        weaponComponents.Add(bow);
        weaponComponents.Add(staff);

        weaponObjects.Add(sword.gameObject);
        weaponObjects.Add(bow.gameObject);
        weaponObjects.Add(staff.gameObject);

        currentWeapon = sword;
    }

    void Update() 
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        position.x = moveInput.x * moveSpeed;
        position.y = rigidBody.velocity.y;
        rigidBody.velocity = position;

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping) 
        {
            isJumping = true;
        }

        if (isJumping) 
        {
            jumpTime += Time.deltaTime;
            position.y = jumpTime < jumpDuration * 0.5f ? jumpSpeed : -jumpSpeed;
            rigidBody.velocity = position;
            
            if (jumpTime >= jumpDuration)
            {
                isJumping = false;
                position.y = 0;
                jumpTime = 0f;
                rigidBody.velocity = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SwapWeapon(0);
        } 
        else if(Input.GetKeyDown(KeyCode.S))
        {
            SwapWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SwapWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentWeapon.Attack();
        }
    }

    private void SwapWeapon(int index)
    {
        currentWeapon = weaponComponents[index];

        for(int i=0; i<weaponObjects.Count; i++)
        {
            weaponObjects[i].SetActive(i == index);
        }
    }
}
