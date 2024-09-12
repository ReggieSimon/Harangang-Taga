using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patotot : MonoBehaviour
{
    public List<GameObject> playersInRange = new List<GameObject>();
    public List<GameObject> closePlayers = new List<GameObject>();
    public GameObject[] defenderObjects;
    public List<Defender> defenders = new List<Defender>();

    public GameObject closestEnemy;

    public GameObject closeCollider;
    public Collider2D rangeCollider;

    public float colliderBounds;
    public float speed;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        defenderObjects = GameObject.FindGameObjectsWithTag("Defender");
        foreach (GameObject d in defenderObjects)
        {
            defenders.Add(d.GetComponent<Defender>());
        }

        closeCollider = this.gameObject.transform.parent.GetChild(1).gameObject;
        rangeCollider = this.gameObject.transform.parent.GetChild(2).gameObject.GetComponent<Collider2D>();

    }

    void Update()
    {
        ClosestEnemy();
        if (closestEnemy != null)
        {
            Animate();
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x, closestEnemy.transform.position.y), speed * Time.deltaTime);
            transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -rangeCollider.bounds.extents.y + rangeCollider.gameObject.transform.position.y, 
                rangeCollider.bounds.extents.y + rangeCollider.gameObject.transform.position.y));
            closeCollider.transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -colliderBounds, colliderBounds));
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            GameManager.Instance.GameOver();
        }
    }

    public void ClosestEnemy()
    {
        float leastDistance = Mathf.Infinity;
        if (closePlayers.Count != 0)
        {
            foreach (GameObject p in closePlayers)
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
                float distanceHere = Vector3.Distance(transform.position, p.transform.position);

                if (distanceHere < leastDistance)
                {
                    leastDistance = distanceHere;
                    closestEnemy = p;
                    CheckSide(p);
                }
            }
        }
        else
        {
            closestEnemy = null;
        }
    }

    public void CheckSide(GameObject player)
    {
        if (player.transform.position.x < this.gameObject.transform.position.x)
        {
            transform.parent.eulerAngles = new Vector3(0, 0, 0);
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = false;
            this.gameObject.transform.GetChild(0).gameObject.transform.position = new Vector2(0.8f, this.gameObject.transform.position.y - -0.4f);
        }
        else
        {
            transform.parent.eulerAngles = new Vector3(0, 180, 0);
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = true;
            this.gameObject.transform.GetChild(0).gameObject.transform.position = new Vector2(1, this.gameObject.transform.position.y - -0.4f);
        }
    }

    public void Animate()
    {
        if (transform.position.y != closestEnemy.transform.position.y)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}