using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody= GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(moveSpeed, 0);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Vector3 scale = transform.localScale;
    //    moveSpeed = -moveSpeed;
    //    scale.x = -scale.x;
    //    transform.localScale = scale;
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        Vector3 scale = transform.localScale;
        moveSpeed = -moveSpeed;
        scale.x = -scale.x;
        transform.localScale = scale;
    }
}
