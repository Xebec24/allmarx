using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 PlayerInput;
    Vector2 CameraInput;
    [SerializeField] float speed;
    [SerializeField] float Sensitivity;
    [SerializeField] Rigidbody Playerbody;
    [SerializeField] CapsuleCollider hitbox;
    [SerializeField] Transform Player;
    [SerializeField] Transform feet;
    // Start is called before the first frame update
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        CameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
    void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerInput) * speed;
        Playerbody.velocity = new Vector3(MoveVector.x, Playerbody.velocity.y, MoveVector.z);

    }
}
