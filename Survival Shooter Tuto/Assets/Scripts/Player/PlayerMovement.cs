using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 movement;
    private Animator anim;
    private Rigidbody rb;
    private int floorMask;
    private float camRayLength = 100f;

    public float speed = 6f;

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    private void Move(float pHori, float pVerti)
    {
        movement.Set(pHori, 0f, pVerti);
        movement = movement.normalized * speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitFloor;

        if (Physics.Raycast(camRay, out hitFloor, camRayLength, floorMask))
        {
            Vector3 playerToMouse = hitFloor.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }
    /*
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }*/

    private void Animating(float pHori, float pVerti)
    {
        anim.SetBool("IsWalking", pHori != 0f || pVerti != 0f);
    }
}
