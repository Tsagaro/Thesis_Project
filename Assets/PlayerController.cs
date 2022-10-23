using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float translateSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //we will add rotation afterwards
        // float rotateAcceleration = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed;
        float verticalSpeed = Input.GetAxis("Vertical") * Time.deltaTime * translateSpeed;
        float horizontalSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * translateSpeed;
        // transform.Rotate(0, 0, -rotateAcceleration);
        transform.Translate(horizontalSpeed, verticalSpeed, 0);
        
    }
}
