using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float xRot;
    Vector3 PlayerInput;
    Vector2 CameraInput;
    [SerializeField] float speed;
    [SerializeField] float Sensitivity;
    [SerializeField] Rigidbody Playerbody;
    [SerializeField] CapsuleCollider hitbox;
    [SerializeField] Transform Player;
    [SerializeField] Transform feet;
    [SerializeField] private LayerMask floormask;
    [SerializeField] private float jumpforce;
    [SerializeField] Camera PlayerCamera;
    public float stamina = 98f;
    public float staminaMax;
    public float time;
    public float timer = 2f;
    public float staminaRegen;
    public float staminaNeg;
    public float NormalSpeed;
    public float sprintSpeed;
    bool isMoving = false;
    void Start()
    {

        NormalSpeed = speed;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if(time < timer)
        {
            time += Time.deltaTime;
        }
        PlayerInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        CameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayer();
        MoveCamera();
        Sprint();
        crouch();
        prone();
        ifYouAreMoving();
    }
    void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerInput) * speed;
        Playerbody.velocity = new Vector3(MoveVector.x, Playerbody.velocity.y, MoveVector.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.CheckSphere(feet.position, 0.1f, floormask))
            {
                Playerbody.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            }
        }

    }
    void MoveCamera()
    {
        xRot -= CameraInput.y * Sensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.Rotate(0f, CameraInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

    }
    void Sprint()
    {
        bool numberStamiaNeg = true;
        bool Shift = Input.GetKey(KeyCode.RightShift) ^ Input.GetKey(KeyCode.LeftShift);
        bool crouch = Input.GetKey(KeyCode.LeftControl) ^ Input.GetKey(KeyCode.RightControl);
        bool prone = Input.GetKey(KeyCode.Z);
        if(Shift && !crouch && !prone && stamina > 0f && numberStamiaNeg)
        {
            speed = sprintSpeed;
        }
        else if (!crouch && !prone)
        {
            speed = NormalSpeed;
        }
        if (Shift && !crouch && !prone && stamina >= 0f && time > 2 && numberStamiaNeg && isMoving) 
        {
            stamina = stamina - staminaNeg;
            time = time - timer;
        }
        if(!Shift && !crouch && !prone && stamina  <= staminaMax && time > 2)
        {
            stamina = stamina + staminaRegen;
            time = time - timer;
        }
        if(stamina < 0)
        {
            stamina = 0f;
        }
        if (stamina == 0f || stamina > 0f && stamina > staminaNeg)
        {
            numberStamiaNeg = true;
        }
        else if (stamina == 0f || stamina > 0f && stamina < staminaNeg)
        {
            numberStamiaNeg = false;
        }

    }
    void crouch()
    {
        bool iscrouch = Input.GetKey(KeyCode.LeftControl) ^ Input.GetKey(KeyCode.RightControl);
        bool isprone  = Input.GetKey(KeyCode.Z);
        if (iscrouch && !isprone)
        {
            speed = 2;
            hitbox.height = 0.5f;
        }
        if (!iscrouch && !isprone)
        {
            speed = NormalSpeed;
            hitbox.height = 2;
        }

    }
    // Prone
    void prone()
    {
        bool iscrouch = Input.GetKey(KeyCode.LeftControl) ^ Input.GetKey(KeyCode.RightControl);
        bool isprone = Input.GetKey(KeyCode.Z);
        if (isprone)
        {
            speed = 1;
            hitbox.height = 0f;
        }
        if (!isprone && !iscrouch)
        {
            hitbox.height = 2;
            speed = NormalSpeed;
        }
    }
    void ifYouAreMoving()
    {
        bool A = Input.GetKey(KeyCode.A);
        bool S = Input.GetKey(KeyCode.S);
        bool D = Input.GetKey(KeyCode.D);
        bool W = Input.GetKey(KeyCode.W);
        if (W || D || S || A)
        {
            isMoving = true;
        }
        if (!D && !W && !S && !A)
        {
            isMoving = false;
        }
    }
}
