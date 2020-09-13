using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 PlayerDirection;


    void Update()
    {
        TakeInput();
        PlayerMove();

    }

    private void TakeInput() {
        PlayerDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            PlayerDirection += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerDirection += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerDirection += Vector2.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerDirection += Vector2.down;
        }



    }
    private void PlayerMove() {
        transform.Translate(PlayerDirection * speed * Time.deltaTime);
    }


}
