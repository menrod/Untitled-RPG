using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] Transform MainCameraTransform;
    [SerializeField] Rigidbody rb;
    [SerializeField] float SpeedLimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float moveforward = 0;
        float heading = Mathf.Atan2(x, y);
        if ((x != 0) || (y != 0))
        {
            Vector3 target = MainCameraTransform.position - transform.position;
            target.y = 0;
            target.x = target.x * x;
            target.z = target.z * y;
            Quaternion targetrot = Quaternion.LookRotation(target);

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetrot, Time.deltaTime * 5);
            moveforward = 1* Mathf.Clamp(Vector2.SqrMagnitude(new Vector2(x, y)), 0, 1);
        }
        Vector3 Movetowards = (transform.forward *moveforward* 0.5f);
        
        rb.AddForce(Movetowards, ForceMode.Impulse);

        Vector3 CurrentSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        Vector3 SpeedLim = Vector3.ClampMagnitude(CurrentSpeed, SpeedLimit);

        rb.velocity = new Vector3(SpeedLim.x, rb.velocity.y, SpeedLim.z);
      
    }
}
