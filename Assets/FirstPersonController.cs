using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float mouseSensitivityX = 250f;
    public float mouseSensitivityY = 250f;
    public float cameraSpeed = 10f;

    public GameObject gm;
    public Rigidbody rb;

    Transform cameraT;
    float verticalLookRotation;

    public float lookSpeed = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(transform.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        transform.localEulerAngles = transform.right * verticalLookRotation;
    }


    
}

    
