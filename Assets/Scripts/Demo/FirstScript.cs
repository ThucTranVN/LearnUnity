using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScript : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake");
    }

    void OnEnable()
    {
        Debug.Log("Enabled");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // FixedUpdate is called multiple per frame
    void FixedUpdate()
    {
        //Dung cho xy ly vat ly
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        // Xu ly input cua nguoi choi
    }

    private void LateUpdate()
    {
        //Xu ly camera
    }

    private void OnDisable()
    {
        Debug.Log("Disabled");
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed");
    }

    //Vector3 direction = Player.position - this.transform.position;
    //Debug.DrawRay(transform.position, direction, Color.red);
    //    //float distance = Vector3.Distance(Player.position, this.transform.position);
    //    Debug.Log("Distance from A to B: " + direction.magnitude);
    //    //direction.Normalize();
    //    transform.Translate(direction* Time.deltaTime);
}
