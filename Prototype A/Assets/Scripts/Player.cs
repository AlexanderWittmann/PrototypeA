using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //transform.Translate(Input.GetAxis("Vertical") * Time.deltaTime * speed, 0f, -Input.GetAxis("Horizontal") * Time.deltaTime * speed);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0f, -moveHorizontal);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            Vector3 current = rb.velocity;
            rb.velocity = -current/(7.5f);
        }
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("YOU LOSE!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(other.gameObject.tag == "Goal")
        {
            Debug.Log("YOU WIN!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
