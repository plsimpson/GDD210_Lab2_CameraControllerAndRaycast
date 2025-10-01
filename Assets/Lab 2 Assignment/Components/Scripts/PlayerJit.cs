using UnityEngine;

public class PlayerJit : MonoBehaviour
{
    public CharacterController Cc;
    public Transform cameraTransform; // Reference only to the camera transform

    public float Gravity = -9.81f;
    public float WalkSpeed = 5f;
    public float JumpSpeed = 20f;
    
    private float yspeed;
    private Vector3 startPosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startPosition = transform.position;
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) );
        cameraTransform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0) );

        if (Cc.isGrounded) // grounded detection
        {
            yspeed = -1;
            if(Input.GetButtonDown("Jump")) // jump
            {
                yspeed = JumpSpeed;
            }
        }
        else // in air
        {
            if(yspeed > 0) // shorten jump if button released
            {
                if(Input.GetButtonUp("Jump"))
                {
                    yspeed *= 0.5f;
                }
            }

            yspeed += Gravity * Time.deltaTime;
        }

        //move controls 
        Vector3 move = Vector3.zero;
        // apply walk vector
        move += Input.GetAxis("Vertical") * transform.forward; // forward W /back S
        move += Input.GetAxis("Horizontal") * transform.right; // right D / left A
        move = move.normalized * WalkSpeed; // normalize diagonal speed
        // apply gravity
        move += new Vector3(0, yspeed, 0);
        


        Cc.Move(move * Time.deltaTime);

        HandleRaycasting();
    }

    /// <summary>
    /// Do Raycasting shit or sum
    /// </summary>
    private void HandleRaycasting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Raycast
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.DrawLine(cameraTransform.position + new Vector3(0, -0.1f, 0), hit.point, Color.red, 1f);
                // Hit button?
                Button hitButton = hit.collider.gameObject.GetComponent<Button>();
                if (hitButton != null)
                {
                    hitButton.Press();
                }
            }
            
            if (hit.transform.CompareTag("Barrel"))
            {
                //Debug.DrawLine(transform.position, hit.point, Color.yellow);
                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ResetTrigger hitTrigger = hit.gameObject.GetComponent<ResetTrigger>();
        if (hitTrigger != null)
        {
            transform.position = startPosition;
            yspeed = -1;
        }
    }

}
