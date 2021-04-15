using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class drive : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public WheelCollider frontWheel;
    public WheelCollider rearWheel;



    float rbVelocityMagnitude;
    float horizontalInput;
    float verticalInput;
    float medRPM;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        medRPM = (frontWheel.rpm + rearWheel.rpm) / 2;
        rbVelocityMagnitude = rb.velocity.magnitude;

        //motorTorque
        if (medRPM > 0)
        {
            rearWheel.motorTorque = verticalInput * rb.mass * 10.0f;
        }
        else
        {
            rearWheel.motorTorque = verticalInput * rb.mass * 1.5f;
        }

        float nextAngle = horizontalInput * 35.0f;
        frontWheel.steerAngle = Mathf.Lerp(frontWheel.steerAngle, nextAngle, 0.125f);


        if (Mathf.Abs(rearWheel.rpm) > 10000)
        {
            rearWheel.motorTorque = 0.0f;
            rearWheel.brakeTorque = rb.mass * 5;
        }
        //
        if (rbVelocityMagnitude < 1.0f && Mathf.Abs(verticalInput) < 0.1f)
        {
            rearWheel.brakeTorque = frontWheel.brakeTorque = rb.mass * 2.0f;
        }
        else
        {
            rearWheel.brakeTorque = frontWheel.brakeTorque = 0.0f;
        }



        Stabilizer();
    }


    void Stabilizer()
    {
        Vector3 axisFromRotate = Vector3.Cross(transform.up, Vector3.up);
        Vector3 torqueForce = axisFromRotate.normalized * axisFromRotate.magnitude * 50;
        torqueForce.x = torqueForce.x * 0.4f;
        torqueForce -= rb.angularVelocity;
        rb.AddTorque(torqueForce * rb.mass * 0.02f, ForceMode.Impulse);

        float rpmSign = Mathf.Sign(medRPM) * 0.02f;
        if (rbVelocityMagnitude > 1.0f && frontWheel.isGrounded && rearWheel.isGrounded)
        {
            rb.angularVelocity += new Vector3(0, horizontalInput * rpmSign, 0);
        }
    }

}
