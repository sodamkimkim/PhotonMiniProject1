using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance
    {
        get
        {
            if(m_instance ==null)
            {
                m_instance = FindObjectOfType<ScoreManager>();
            }
            return m_instance;
        }
    }
    private static ScoreManager m_instance;
    public enum eTeamName { BlueTeam, RedTeam }

    // Blue goal post obj�� �־��ֱ�
    [SerializeField]
    Team blueTeam = null;
    // Red goal post obj�� �־��ֱ�
    [SerializeField]
    Team redTeam = null;

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void SetScore(Team _team, UserInfo _userInfo, int _score)
    {
        if (_team.GetTeamName().Equals(blueTeam.GetTeamName()))
        { // blueteam goal == blueteam player
            blueTeam.SetTeamScore(_score);
            UIManager.instance.UpdateBlueteamScore(blueTeam.GetTeamScore());
        }
        if (_team.GetTeamName().Equals(redTeam.GetTeamName()))
        { // redteam goal == redteam player
            redTeam.SetTeamScore(_score);
            UIManager.instance.UpdateRedteamScore(redTeam.GetTeamScore());
        }
        // ���� ���� ������ ������Ʈ����
        _userInfo.SetInGamePersonalScore(_score);
    }

    public override string ToString()
    {
        return "BlueTeam: " + blueTeam.GetTeamScore() + " RedTeam: " + redTeam.GetTeamScore();
    }
} // end of class
