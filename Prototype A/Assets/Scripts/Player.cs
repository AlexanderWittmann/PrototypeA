using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        transform.Translate(Input.GetAxis("Vertical") * Time.deltaTime * speed, 0f, - Input.GetAxis("Horizontal")*Time.deltaTime*speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("here1");
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("here2");
            rb.velocity = Vector3.zero;
            
        }
    }
}
