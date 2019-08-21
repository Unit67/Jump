using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _CH;
    private float _H = 0.0f;
    private float _V = 0.0f;
    private float _Grav = 0.0f;
    public float GravSpeed = 5;
    public float _MoveSpeed = 5;
    private Vector3 _CamForward;
    public GameObject Cam;
    private bool _AnGravity = false;
    private float _AnGravSpeed = 2;
    private KeyCode _JumpKey = KeyCode.Space;
    private KeyCode _SpeedKey = KeyCode.LeftShift;
    private RaycastHit _hit;

    float SpeedH = 2.0f, SpeedV = 2.0f;
    float Yaw = 0.0f, Pitch = 0.0f;
    private void Start()
    {
        _CH = this.gameObject.GetComponent<CharacterController>();
        //Cam = Camera.current;
    }
    private void Update()
    {
        #region Move Region
        _H = Input.GetAxis("Horizontal");
        _V = Input.GetAxis("Vertical");

        if (_AnGravity == false)
        {
            _Grav -= GravSpeed * Time.deltaTime;
            if(Physics.Raycast(transform.position,-transform.up, out _hit, 2))
            {
                Debug.DrawLine(transform.position, _hit.point, Color.black);
                if(_hit.collider.name == "building")
                {
                    _Grav = 0;
                    GravSpeed = 5;
                    _MoveSpeed = 10;
                }
            }
        }
        else
        {
            _Grav += _AnGravSpeed * Time.deltaTime;
        }


        if (Input.GetKey(_JumpKey))
        {
            _AnGravity = true;
            _MoveSpeed = 20;
        }
        else
        {
            _AnGravity = false;
            _MoveSpeed = 20;
            if(_hit.collider != null)
            {
                if (_hit.collider.name == "building")
                {
                    GravSpeed = 1;
                    _MoveSpeed = 10;
                }
            }
        }

        _CamForward = Vector3.Scale(Cam.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 MoveDirection = (_V * _CamForward + _H * Cam.transform.right).normalized;
        MoveDirection *= Time.deltaTime;
        MoveDirection *= _MoveSpeed;

        MoveDirection.y = _Grav;

        _CH.Move(MoveDirection);
        #endregion

        #region Cam

        Yaw -= SpeedH * Input.GetAxis("Mouse Y");
        Pitch += SpeedV * Input.GetAxis("Mouse X");

        Cam.transform.eulerAngles = new Vector3(Yaw,Pitch,0);

        #endregion

    }
}
