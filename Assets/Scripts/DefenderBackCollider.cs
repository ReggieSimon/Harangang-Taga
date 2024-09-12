using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderBackCollider : MonoBehaviour
{
    public Defender defender;

    public List<GameObject> playersInRange = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        defender = transform.parent.GetChild(0).GetComponent<Defender>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<PlayerMovement>().endReach)
        {
            transform.parent.GetChild(0).GetComponent<Defender>().playersInBack.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(transform.parent.GetChild(0).GetComponent<Defender>().playersInBack.Contains(collision.gameObject))
            {
                transform.parent.GetChild(0).GetComponent<Defender>().playersInBack.Remove(collision.gameObject);
            }           
        }
    }
}
