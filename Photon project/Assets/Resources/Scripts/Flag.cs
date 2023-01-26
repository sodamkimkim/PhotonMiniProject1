using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private Transform flagTr = null;
    private Vector3 flagOriginPos = Vector3.zero;
    private void Awake()
    {
        flagTr = GetComponent<Transform>();
        flagOriginPos = flagTr.position;
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("BluePlayer") || _other.gameObject.CompareTag("RedPlayer"))
        {
            Debug.Log(_other.tag.ToString() + "�� flag�� �����ߴ�!");
            PlayerMove playerMove = _other.gameObject.GetComponent<PlayerMove>();
            Transform playerTr = playerMove.GetPlayerTr();
            SetParentWithPlayer(playerTr);
        }
    }
    // flag�� player�� �浹 ���� �� ȣ��Ǵ� �Լ�
    private void SetParentWithPlayer(Transform _parentTr)
    {
        flagTr.SetParent(_parentTr);
        SetFlagWithPlayer(_parentTr);
        //flagTr.SetParent(null);
        //SetFlagWithNoPlayer(flagOriginPos);

    }

    // player�� flag�� �浹���� �� flag�� ��ġ, ������, rot��ȭ
  private void   SetFlagWithPlayer(Transform _parentTr)
    {
        //Vector3 newPos = _parentTr.position;
        Vector3 newPos = Vector3.zero;
        newPos.x += 0.362f;
        newPos.y += 1.028f;
        newPos.z += -0.093f;
        flagTr.localPosition = newPos;


        flagTr.localScale = new Vector3(0.3f, 0.4f, 0.3f);
        Vector3 eulerAngle = _parentTr.rotation.eulerAngles;
        eulerAngle.x += 90f;
        eulerAngle.z += 90f;
        flagTr.rotation = Quaternion.Euler(eulerAngle);
    }

    // flag�� �ʻ�������� position���� ����
    private void SetFlagOriginPos(Vector3 _flagOriginPos)
    {
        Vector3 newPos = _flagOriginPos;
        flagTr.position = newPos;

        flagTr.localScale = new Vector3(1f, 1f, 1f);
        flagTr.rotation = Quaternion.Euler(0f, 0f, 0f);
    }



} // end of calss
