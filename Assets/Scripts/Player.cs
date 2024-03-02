using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private bool isWalking;
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {

        Vector2 inputVec = new Vector2(0, 0);

        if(Input.GetKey(KeyCode.W)) inputVec.y = +1f;

        if (Input.GetKey(KeyCode.A)) inputVec.x = -1f;

        if(Input.GetKey(KeyCode.S)) inputVec.y = -1f;

        if (Input.GetKey(KeyCode.D)) inputVec.x = +1f;

        inputVec = inputVec.normalized;

        Vector3 moveDir = new Vector3(inputVec.x, 0f, inputVec.y);

        isWalking = moveDir != Vector3.zero;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        //Face the movement direction
        float rotSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotSpeed);
    }

    public bool IsWalking => isWalking;
}
