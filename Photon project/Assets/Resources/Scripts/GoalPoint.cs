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
            // 다른팀에 점수 주는 거 허용x
            Flag flag = _other.gameObject.GetComponentInChildren<Flag>();
            if (flag != null)
            {
                    UserInfo userInfo = _other.gameObject.GetComponent<UserInfo>();
                Debug.Log("깃발가진 player다!");

                if (this.gameObject.CompareTag(ScoreManager.eTeamName.BlueTeam.ToString())) // bluteam post일 때 여기 실행
                {
                    if (_other.gameObject.CompareTag("BluePlayer"))
                    {
                        Team blueTeam = _other.gameObject.GetComponent<BlueTeam>();
                    GetScoreActions(blueTeam, userInfo);
                        flag.SetFlagOriginPos();
                    }
                    else
                    {
                        Debug.Log("여기는 BlueTeam post다!!!");
                    }

                }
                if (this.gameObject.CompareTag(ScoreManager.eTeamName.RedTeam.ToString())) // redteam post일 때 여기 실행
                {
                    if (_other.gameObject.CompareTag("RedPlayer"))
                    {
                        Team redTeam = _other.gameObject.GetComponent<RedTeam>();
                        GetScoreActions(redTeam, userInfo);
                        flag.SetFlagOriginPos();

                    }
                    else
                    {
                        Debug.Log("여기는 RedTeam post다!!!");
                    }
                }

            }
            else
            {
                Debug.Log("Player인데 깃발은 없네");
            }

        }

    }
    private void GetScoreActions(Team _team, UserInfo _userInfo)
    {
        Transform userTr = _userInfo.gameObject.GetComponent<Transform>();
 
        Debug.Log(_team.GetTeamName() + " 1점 획득!");
        // Team정보,  player정보랑 점수 ScoreManager에 던져줌
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
