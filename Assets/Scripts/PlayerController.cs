using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    [SerializeField] protected Rigidbody2D rigidbody2d;

    [SerializeField] protected Vector2 move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //         QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 10;
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);

    }
    
    void FixedUpdate()
    {
          Vector2 position = (Vector2)transform.position + move * 3f * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }


}

