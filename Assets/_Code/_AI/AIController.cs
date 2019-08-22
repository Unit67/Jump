using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject Player;

    public CharacterController characterController;
    public float AIFov = 60;
    public float ViewRange = 10;
    private void Update()
    {
        RaycastHit Hit;
        Vector3 RayDirection = Player.transform.position - this.gameObject.transform.position;
        if(Vector3.Angle(RayDirection,transform.forward) < AIFov)
        {
            if(Physics.Raycast(transform.position,RayDirection,out Hit,ViewRange))
            {
                Aspect aspect = Hit.collider.GetComponent<Aspect>();
                if(aspect != null)
                {
                    if(aspect.aspectName == Aspect.aspect.Player)
                    {
                        Debug.Log("Player Detected");
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (Player == null)
        {
            return;
        }

        Debug.DrawLine(transform.position, Player.transform.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * ViewRange);

        Vector3 leftRayPoint = frontRayPoint;
        leftRayPoint.x += AIFov * 0.5f;

        Vector3 rightRayPoint = frontRayPoint;
        rightRayPoint.x -= AIFov * 0.5f;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }
}
