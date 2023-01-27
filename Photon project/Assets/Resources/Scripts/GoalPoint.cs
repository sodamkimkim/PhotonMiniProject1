using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject cellebrationPrefab = null;
    [SerializeField]
    private ScoreManager scoreManager = null;
    private const int flagScore = 1;
    private void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.CompareTag("BluePlayer") || _other.gameObject.CompareTag("RedPlayer"))
        {
            // �ٸ����� ���� �ִ� �� ���x
            Flag flag = _other.gameObject.GetComponentInChildren<Flag>();
            if (flag != null)
            {
                    UserInfo userInfo = _other.gameObject.GetComponent<UserInfo>();
                Debug.Log("��߰��� player��!");

                if (this.gameObject.CompareTag(ScoreManager.eTeamName.BlueTeam.ToString())) // bluteam post�� �� ���� ����
                {
                    if (_other.gameObject.CompareTag("BluePlayer"))
                    {
                        Team blueTeam = _other.gameObject.GetComponent<BlueTeam>();
                    GetScoreActions(blueTeam, userInfo);
                        flag.SetFlagOriginPos();
                    }
                    else
                    {
                        Debug.Log("����� BlueTeam post��!!!");
                    }

                }
                if (this.gameObject.CompareTag(ScoreManager.eTeamName.RedTeam.ToString())) // redteam post�� �� ���� ����
                {
                    if (_other.gameObject.CompareTag("RedPlayer"))
                    {
                        Team redTeam = _other.gameObject.GetComponent<RedTeam>();
                        GetScoreActions(redTeam, userInfo);
                        flag.SetFlagOriginPos();

                    }
                    else
                    {
                        Debug.Log("����� RedTeam post��!!!");
                    }
                }

            }
            else
            {
                Debug.Log("Player�ε� ����� ����");
            }

        }

    }
    private void GetScoreActions(Team _team, UserInfo _userInfo)
    {
        Transform userTr = _userInfo.gameObject.GetComponent<Transform>();
 
        Debug.Log(_team.GetTeamName() + " 1�� ȹ��!");
        // Team����,  player������ ���� ScoreManager�� ������
        scoreManager.SetScore(_team, _userInfo, flagScore);
        LaunchCellebration(userTr);
        Debug.Log(scoreManager.ToString());
    }
    private void LaunchCellebration(Transform _playerTr)
    {
        GameObject go = Instantiate(cellebrationPrefab, _playerTr.position, Quaternion.identity);
        go.transform.SetParent(_playerTr);
        Destroy(go, 3);
    }
} // end of class
