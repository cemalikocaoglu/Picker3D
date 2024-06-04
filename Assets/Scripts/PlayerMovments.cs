using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovments : MonoBehaviour
{

    [SerializeField] public Rigidbody _rigidbody;
    [SerializeField] float speed = 10;
    public Camera _camera;
    public Vector3 _position;

    private void Start()
    {
       _rigidbody = GetComponent<Rigidbody>();
    }



    private void FixedUpdate()
    {
        _rigidbody.velocity += new Vector3(Input.GetAxisRaw("Horizontal") , 0 , 
            Input.GetAxisRaw("Vertical")) * (speed * Time.fixedDeltaTime);



       _camera.transform.position =  new Vector3(transform.position.x + _position.x , transform.position.y +_position.y
           , transform.position.z + _position.z);

    }

    

}
