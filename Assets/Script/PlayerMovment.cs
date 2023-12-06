using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator _animator;

    public Rigidbody _rb;

    public float walkSpeed;
    public float runSpeed;

    private float lastTapTime = 0;
    private float doubleTabThreshold = 0.3f;

    private bool isDoubleTab;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
        }
        else if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if(Time.time - lastTapTime <= doubleTabThreshold) //DoubleTab Check
                {
                    lastTapTime = 0;
                    isDoubleTab = true;
                }
                else
                {
                    lastTapTime = Time.time;
                    isDoubleTab = false;
                }
            }

            if(!isDoubleTab)
            {
                _animator.SetBool("isWalking", true);
                _animator.SetBool("isRunning", false);
                walk();
            }
            else
            {
                _animator.SetBool("isWalking", false);
                _animator.SetBool("isRunning", true);
                run();
            }
        }
    }

    public void walk()
    {
        _rb.velocity = Vector3.forward * walkSpeed;
    }
    public void run()
    {
        _rb.velocity = Vector3.forward * runSpeed;
    }

}
