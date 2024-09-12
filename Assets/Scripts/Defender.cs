using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    public List<GameObject> playersInRange = new List<GameObject>();
    public List<GameObject> playersInCloseRange = new List<GameObject>();
    public List<GameObject> playersInBack = new List<GameObject>();
    public GameObject closestEnemy;

    public bool lastDefender;
    public bool firstDefender;

    public bool rightFocus;
    public bool leftFocus;

    public bool faceBack;

    private float sideLureBoundary = 2f;

    public float speed;
    public float lastDefenderDistance;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastDefender)
        {
            LastDefenderClosestEnemy();
        }
        else
        {
            ClosestEnemy();   
        }

        if (closestEnemy != null)
        {
            Animate();
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(closestEnemy.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isBack", false);
        }
        CheckFace();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            GameManager.Instance.GameOver();
        }
    }

    public void ClosestEnemy()
    {
        float leastDistance = Mathf.Infinity;
        float xPos;

        if (rightFocus)
        {
            xPos = sideLureBoundary;
        }
        else if (leftFocus)
        {
            xPos = -sideLureBoundary;
        }
        else
        {
            xPos = transform.position.x;
        }

        if (playersInCloseRange.Count != 0)
        {
            foreach (GameObject p in playersInCloseRange)
            {
                float distanceHere = Vector3.Distance(transform.position, p.transform.position);

                if (distanceHere < leastDistance)
                {
                    leastDistance = distanceHere;
                    closestEnemy = p;
                }
            }
        }
        else if (playersInRange.Count != 0)
        {
            foreach (GameObject p in playersInRange)
            {
                float distanceHere = Vector3.Distance(new Vector2(xPos, transform.position.y), p.transform.position);

                if (distanceHere < leastDistance)
                {
                    leastDistance = distanceHere;
                    closestEnemy = p;
                }
            }
        }
        else if(playersInBack.Count != 0)
        {
            foreach (GameObject p in playersInBack)
            {
                float distanceHere = Vector3.Distance(transform.position, p.transform.position);

                if (distanceHere < leastDistance)
                {
                    leastDistance = distanceHere;
                    closestEnemy = p;
                }
            }
        }
        else
        {
            closestEnemy = null;
        }
    }

    public void LastDefenderClosestEnemy()
    {
        float leastDistance = Mathf.Infinity;

        //checcks if player in back is close

        if (playersInCloseRange.Count != 0)
        {
            foreach (GameObject p in playersInCloseRange)
            {
                float distanceHere = Vector3.Distance(transform.position, p.transform.position);

                if (distanceHere < leastDistance)
                {
                    leastDistance = distanceHere;
                    closestEnemy = p;
                }
            }
        }
        else if (playersInBack.Count != 0)
        {
            foreach (GameObject p in playersInBack)
            {
                float distanceHere = Vector3.Distance(transform.position, p.transform.position);

                //prevents players from prioritizing far away players
                if (distanceHere < leastDistance && distanceHere < lastDefenderDistance)
                {
                    leastDistance = distanceHere;
                    closestEnemy = p;
                }
            }
        }
        else if (playersInRange.Count != 0)
        {
            foreach (GameObject p in playersInRange)
            {
                float distanceHere = Vector3.Distance(transform.position, p.transform.position);

                if (distanceHere < leastDistance && !p.GetComponent<PlayerMovement>().endReach)
                {
                    leastDistance = distanceHere;
                    closestEnemy = p;
                }
            }
        }
        else if (playersInBack.Count != 0)
        {
            foreach (GameObject p in playersInBack)
            {
                float distanceHere = Vector3.Distance(transform.position, p.transform.position);

                if (distanceHere < leastDistance)
                {
                    leastDistance = distanceHere;
                    closestEnemy = p;
                }
            }
        }
        else
        {
            closestEnemy = null;
        }
    }

    public void CheckFace()
    {
        if(closestEnemy != null)
        {
            if (closestEnemy.transform.position.y <= transform.position.y)
            {
                faceBack = false;
            }

            else
            {
                faceBack = true;
            }
            animator.SetBool("isBack", faceBack);
        }
        
    }

    public void Animate()
    {
        if (transform.position.x != closestEnemy.transform.position.x)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if(transform.position.x <= closestEnemy.transform.position.x)
        {
            animator.SetBool("isRight", true);
        }

        else
        {
            animator.SetBool("isRight", false);
        }
    }
}
