using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FirstScript : MonoBehaviour
{
    public float speed;
    public float basicSpeed;
    public float runningSpeed;
    public float rotationSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
        speed=basicSpeed;

        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        rb.AddRelativeTorque(0, mouseX * rotationSpeed * Time.deltaTime, 0);


        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.back * speed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            speed=runningSpeed;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            speed=basicSpeed;
        }
    }
}
