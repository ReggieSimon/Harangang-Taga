using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatotoRangeCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.GetChild(0).GetComponent<Patotot>().playersInRange.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.parent.GetChild(0).GetComponent<Patotot>().playersInRange.Contains(collision.gameObject))
            {
                transform.parent.GetChild(0).GetComponent<Patotot>().playersInRange.Remove(collision.gameObject);
            }

        }
    }
}
