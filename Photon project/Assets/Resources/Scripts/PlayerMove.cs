using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{

    private Color playerColor = new Color(0, 0, 256);

    private const float runSpeed = 4f;
    private const float rotSpeed = 100f;

    Transform playerTr = null;
    private NavMeshAgent agent = null;
    private LayerMask layerMask;

    private bool isDead = false;

    private Animator anim = null;
    private bool IsForward = true;

    bool IsWalkable = true;

    private void Awake()
    {
        playerTr = this.GetComponent<Transform>();
        anim = GetComponent<Animator>();

    }
    private void Start()
    {
        isDead = false;
    }
    private void Update()
    {
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsFallingDown", false);
        anim.SetBool("IsWalking", false);

        if (Input.GetKey(KeyCode.UpArrow)&& IsWalkable)
        {
            anim.SetBool("IsRunning", true);

                if (!IsForward)
                {
                    IsForward = true;
                }
                playerTr.position = transform.position + (transform.forward * runSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow) && IsWalkable)
        {
            anim.SetBool("IsRunning", true);
            if (IsForward )
            {
                IsForward = false;
            }
            playerTr.position = transform.position + (-transform.forward * runSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            IsWalkable = true;
            anim.SetBool("IsWalking", true);
            playerTr.Rotate(-Vector3.up, rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            IsWalkable = true;
            anim.SetBool("IsWalking", true);
            playerTr.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space) && IsWalkable)
        {
            float axisJ = Input.GetAxis("Jump");
            transform.Translate(Vector3.up * axisJ * runSpeed * 1.5f * Time.deltaTime);
        }

    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Flag"))
        {
            Debug.Log("flag");
        }

    }

    private void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Floor");
            IsWalkable = true;
        }
        if (_other.gameObject.CompareTag("NotWalkable"))
        {
            IsWalkable = false;
            Debug.Log("NotWalkable");
            playerTr.position = transform.position + (-transform.forward * runSpeed*1.2f * Time.deltaTime);
        }

    }
    private void OnCollisionExit(Collision _other)
    {
        //if (_other.gameObject.CompareTag("NotWalkable"))
        //{
  
        //    IsWalkable = true;
        //    Debug.Log("NotWalkable Exit");
    
        //}
    }

} // end of class
