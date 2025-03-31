using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackpoint;
    public GameObject objectToThrow;
    [Header("Settings")]
    public int totalThrow;
    public float Throwcooldown;
    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;
    private bool hit;

    bool readytothrow;
    private void Start()
    {
        readytothrow = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(throwKey) && readytothrow && totalThrow > 0)
        {
            Throw();
        }
    }
    private void Throw()
    {
        readytothrow = false;
        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackpoint.position, cam.rotation);
        // get RigidBody component
        Rigidbody ProjectileRb = projectile.GetComponent<Rigidbody>();
        // calculate direction
        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackpoint.position).normalized;
        }
        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        ProjectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        totalThrow--;
        // implement throwCooldown
        Invoke(nameof(ResetThrow), Throwcooldown);

    }
    private void ResetThrow()
    {
        readytothrow = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        
        // Check if the collided object has a specific tag or component
        if (collision.gameObject.CompareTag("dagger")) // Replace "CloneTag" with the actual tag of your clones
        {
            Destroy(collision.gameObject);  // Destroy the collided object (clone)
        }

    }
}
