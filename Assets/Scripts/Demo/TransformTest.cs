using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float moveSpeed;

    void Update()
    {
        //Vector huong = Vi tri dich den - vi tri nguon
        Vector3 direction = playerTransform.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        Debug.Log("Distance from Enemy to Player: " + direction.magnitude);

        //float distance = Vector3.Distance(transform.position, playerTransform.position);
        //Debug.Log("Distance from Enemy to Player - 2: " + distance);
        direction.Normalize();
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }
}
