using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float jumpForce = 10.0f;
    float gravityModifier = 2.0f;
    float moveSpeed = 25.0f;
    bool Grounded;
    Rigidbody rb;
    Renderer rndr;
    public Material[] mtrls;
    int xLimit = 20;
    int zLimit = 20;

    // Start is called before the first frame update
    void Start()
    {
        Grounded = true;
        Physics.gravity *= gravityModifier;
        rb = GetComponent<Rigidbody>();
        rndr = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * vertical * moveSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontal * moveSpeed);

        if (transform.position.z <-zLimit)
		{
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
            rndr.material.color = mtrls[5].color;
		}
        else if (transform.position.z > zLimit)
		{
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            rndr.material.color = mtrls[4].color;
		}
        if (transform.position.x < -xLimit)
		{
            transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
            rndr.material.color = mtrls[3].color;
		}
        else if (transform.position.x > xLimit)
		{
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
            rndr.material.color = mtrls[2].color;
		}
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("GamePlane"))
		{
            Grounded = true;
            rndr.material.color = mtrls[1].color;
		}
	}
    
    private void PlayerJump()
	{
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Grounded = false;

            rndr.material.color = mtrls[0].color;
        }
    }
}
