using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour, IPointerClickHandler
{
    public float moveSpeed;
    public Rigidbody2D rb;

    public bool selected;
    public bool endReach;

    public Animator animator;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            if (Time.timeScale != 0)
            {
                ProcessInputs();
            }
            
        }  
    }

    void FixedUpdate()
    {
        if (selected)
        {
            Move();
        }
        else
        {
            animator.SetFloat("Speed", 0);
            rb.velocity = Vector2.zero;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.timeScale != 0)
        {
            GameManager.Instance.UnselectPlayers();
            selected = true;
        }   
    }
    private void OnMouseDown()
    {
        if (Time.timeScale != 0)
        {
            GameManager.Instance.UnselectPlayers();
            selected = true;
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", new Vector2(moveX, moveY).sqrMagnitude);
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
