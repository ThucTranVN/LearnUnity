using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEsta : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private Vector3 mousePosition;

    void Update()
    {
        // Get the position of the mouse in the world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate the direction from the bow to the mouse position
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the bow sprite smoothly towards the mouse position
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
