using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField]
    private Transform playerSpawnPoint;
    private Animator animator;
    private Rigidbody2D rb;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            playerSpawnPoint = collision.gameObject.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_DEATH);
        }
        //khong cho nhan vat di chuyen
        rb.bodyType = RigidbodyType2D.Static;
        //Play death animation
        animator.SetTrigger("Death");
    }

    private void ReSpawn()
    {
        //Set vi tri nhan vat ve vi tri ban dau
        this.transform.position = playerSpawnPoint.position;
        //Cho nhan vat di chuyen
        rb.bodyType = RigidbodyType2D.Dynamic;
        //reset animation state
        animator.Rebind();
    }

}
