using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float movingSpeed = 2f;
    [SerializeField]
    private GameObject[] wayPoints;
    private int curWayPointIndex = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = player.GetComponent<Animator>();
    }

    void Update()
    {
        if(Vector2.Distance(this.transform.position, player.transform.position) >= 3f)
        {
            if (Vector2.Distance(wayPoints[curWayPointIndex].transform.position, transform.position) < 0.1f)
            {
                curWayPointIndex++;
                if (curWayPointIndex >= wayPoints.Length)
                {
                    curWayPointIndex = 0;
                }
            }

            if(animator != null)
            {
                animator.SetTrigger("Move");
            }

            transform.position = Vector2.MoveTowards(transform.position,
                wayPoints[curWayPointIndex].transform.position,
                movingSpeed * Time.deltaTime);
        }
        else
        {
            movingSpeed = 4f;

            if(animator != null)
            {
                animator.SetTrigger("Run");
            }

            transform.position = Vector2.MoveTowards(transform.position,
                player.position,
                movingSpeed * Time.deltaTime);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(animator != null)
            {
                animator.SetTrigger("Attack");
            }
        }
    }


    private void Attactk()
    {
        //Gay dame cho nhan vat
    }
}
