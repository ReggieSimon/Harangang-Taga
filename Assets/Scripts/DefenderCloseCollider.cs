using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderCloseCollider : MonoBehaviour
{
    public Defender defender;

    // Start is called before the first frame update
    void Start()
    {
        defender = transform.parent.GetComponent<Defender>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            if (defender.faceBack)
            {
                if (defender.gameObject.transform.position.y < collision.gameObject.transform.position.y)
                {
                    transform.parent.GetComponent<Defender>().playersInCloseRange.Add(collision.gameObject);
                }
            }

            else
            {
                if (defender.gameObject.transform.position.y > collision.gameObject.transform.position.y)
                {
                    transform.parent.GetComponent<Defender>().playersInCloseRange.Add(collision.gameObject);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.parent.GetComponent<Defender>().playersInCloseRange.Contains(collision.gameObject))
            {
                transform.parent.GetComponent<Defender>().playersInCloseRange.Remove(collision.gameObject);
            }

        }
    }
}
