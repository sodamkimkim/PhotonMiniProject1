using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using ExitGames.Client.Photon;
using System;

public class PlayerCtrl : MonoBehaviourPun
{
    private const float runSpeed = 4f;
    private const float jumpSpeed = 6.5f;
    private const float rotSpeed = 100f;
    Camera cm = null;
    Transform playerTr = null;

    private bool isDead = false;

    private Animator anim = null;
    private bool IsForward = true;
    //  private bool IsJumping = false;

    bool IsWalkable = true;

    GameObject frontCamGo = null;

    private void Awake()
    {
     //   PhotonPeer.RegisterType(typeof(Transform), 0, TransformSerialization.Serialize, TransformSerialization.Deserialize);

        if (!photonView.IsMine) return;
        cm = Camera.main;
        cm.transform.parent = this.gameObject.transform;
        cm.transform.localPosition = new Vector3(0f, 2.67f, -4.12f);
        cm.transform.rotation = Quaternion.Euler(20.546f, 0f, 0f);
        // 0, 1.635, -1.78
        //20.546, 0, 0

        frontCamGo = GameObject.FindWithTag("FrontCam");
        frontCamGo.transform.parent = this.gameObject.transform;
        frontCamGo.transform.localPosition = new Vector3(-0.009999f, 0.83f, 1.11f);
        frontCamGo.transform.rotation = Quaternion.Euler(9.35f, 180f, 0f);

        //        frontCam = frontCamGo.GetComponent<Camera>();
    }
    private void Start()
    {
        isDead = false;
        playerTr = this.GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!photonView.IsMine) return;

        anim.SetBool("IsRunning", false);
        anim.SetBool("IsFallingDown", false);
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsForwardJumping", false);

        if (Input.GetKey(KeyCode.UpArrow) && IsWalkable)
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
            if (IsForward)
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
            anim.SetBool("IsForwardJumping", true);
            float axisJ = Input.GetAxis("Jump");
            transform.Translate(Vector3.up * axisJ * jumpSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.CompareTag("Floor") || _other.gameObject.CompareTag("Wall"))
        {
            IsWalkable = true;
        }

        if (_other.gameObject.CompareTag("NotWalkable"))
        {
            IsWalkable = false;
            Debug.Log("NotWalkable");
            playerTr.position = transform.position + (-transform.forward * runSpeed * 1.7f * Time.deltaTime);
        }
        if (_other.gameObject.CompareTag("BluePlayer") || _other.gameObject.CompareTag("RedPlayer"))
        {
            Flag flag = this.GetComponentInChildren<Flag>();
            if (flag != null)
            {
                Debug.Log("깃발 뺏긴다!");
            }

        }


    }
    //private void OnTriggerStay(Collider _other)
    //{
    //    if (_other.gameObject.CompareTag("Flag"))
    //    {
    //        if (Input.GetKey(KeyCode.F))
    //        {
    //            Debug.LogError(this.gameObject.name + " 이 flag를 차지했다.");
    //            Flag flag = _other.GetComponent<Flag>();

    //            //TODO

    //            flag.photonView.RPC("SetParentWithPlayer", RpcTarget.All, transform);
    //        //    flag.SetParentWithPlayer(transform);
    //        }

    //    }
    //}
    public Transform GetPlayerTr()
    {
        return playerTr;
    }
    public float GetPlayerPosX()
    {
        return playerTr.position.x;
    }
    public float GetPlayerPosY()
    {
        return playerTr.position.y;
    }
    public float GetPlayerPosZ()
    {
        return playerTr.position.z;
    }
    public Quaternion GetPlayerRot()
    {
        return playerTr.rotation;
    }
    public override string ToString()
    {
        return this.gameObject.name;
    }
} // end of class
