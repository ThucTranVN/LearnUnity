using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Chay 1 lan
        Debug.Log(collision.gameObject.name + " Exit");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Chay nhieu lan
        Debug.Log(collision.gameObject.name + " Stay");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Chay 1 lan
        Debug.Log(collision.gameObject.name + " Enter");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " Exit");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " Stay");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " Enter");
    }


    private void Update()
    {
        
    }
}
