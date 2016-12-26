using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Transform model;

    public Vector3 gravity;
    public float gravityModefier;
    public float acceleration;
    public float deacceleration;
    public float maxSpeed;
    public float jumpForce;
    public LayerMask groundMask;

    private Vector3 inputBuffer;
    private Rigidbody body;
    private bool grounded;

	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        // Check if grounded
        CheckGrounded();

        // Do movement if input buffer is set
        Vector3 movement = inputBuffer;
        if (inputBuffer != Vector3.zero)
        {
            // Scale magnitude
            movement.y *= jumpForce;
            movement.x *= acceleration;
            movement.z *= acceleration;

            // Is the player grounded?
            if (!grounded)
            {
                movement.y = 0.0f;
            }

            // Clear input buffer
            inputBuffer = Vector3.zero;

            // Project the local velocity onto the models transform direction
            if (Vector3.Dot(transform.forward, model.right) < 0.0f)
            {
                movement = Quaternion.AngleAxis(Vector3.Angle(transform.forward, model.forward), Vector3.up) * movement;
            }
            else
            {
                movement = Quaternion.AngleAxis(-Vector3.Angle(transform.forward, model.forward), Vector3.up) * movement;
            }
        }

        // Add gravity
        movement += gravity * gravityModefier;

        // Deacceleration of the current velocity
        Vector3 vel = body.velocity;
        vel.x *= deacceleration;
        vel.z *= deacceleration;
        body.velocity = vel;

        // Add the new force
        if(movement != Vector3.zero)
        {
            body.AddForce(movement);
        }
	}

    private void CheckGrounded()
    {
        bool grounded = false;

        RaycastHit hit;
        float rayDist = 0.15f;
        float rayRad = 0.5f;
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + rayRad + 0.1f, transform.position.z), -transform.up);

        if(Physics.SphereCast(ray, rayRad, out hit, rayDist, groundMask))
        {
            if(body.velocity.y <= 0.0f)
            {
                grounded = true;
            }
        }
        this.grounded = grounded;
    }

    public void UpdateInputBuffer(Vector3 newVec)
    {
        inputBuffer = newVec;
    }

    public void Rotate(float amount)
    {
        model.Rotate(transform.up, amount);
    }
}
