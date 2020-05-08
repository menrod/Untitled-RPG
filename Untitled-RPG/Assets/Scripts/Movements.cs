using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] Transform MainCameraTransform;
    [SerializeField] Rigidbody rb;
    [SerializeField] float SpeedLimit, AttackPressed;
    [SerializeField] Animator PlayerAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackPressed = 1;
            rb.AddForce(transform.forward*5, ForceMode.Impulse);
            Vector3 CurrentSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            Vector3 SpeedLim = Vector3.ClampMagnitude(CurrentSpeed, 0.1f);

            rb.velocity = new Vector3(SpeedLim.x, rb.velocity.y, SpeedLim.z);
        }
        else
        {
            if (!PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && !PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                AttackPressed = 0;
                Movement();
            }
            else
            {
                //rb.velocity = Vector3.zero;
            }
        }
        PlayerAnim.SetFloat("Attack", AttackPressed);

    }
    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float moveforward = 1 * Mathf.Clamp(Vector2.SqrMagnitude(new Vector2(x, y)), 0, 1);
        
        if ((x != 0) || (y != 0))
        {
            Vector3 target2 = transform.position - MainCameraTransform.position;
            target2.y = 0;
            Vector3 target1 = new Vector3(x, 0, y);
            
            Quaternion targetrot = Quaternion.LookRotation(target1)* Quaternion.LookRotation(target2);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetrot, Time.deltaTime * 10);
           
        }

        Vector3 Movetowards = (transform.forward * moveforward * 0.5f);
        rb.AddForce(Movetowards, ForceMode.Impulse);

        Vector3 CurrentSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        Vector3 SpeedLim = Vector3.ClampMagnitude(CurrentSpeed, SpeedLimit);

        rb.velocity = new Vector3(SpeedLim.x, rb.velocity.y, SpeedLim.z);

        float PlayerSpeed = Vector2.SqrMagnitude(new Vector2(rb.velocity.x, rb.velocity.z));
        PlayerAnim.SetFloat("PlayerMovementSpeed", PlayerSpeed);
        
    }
}
