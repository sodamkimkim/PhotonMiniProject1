using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public enum eTeamName { BlueTeam, RedTeam }

    // Blue goal post obj를 넣어주기
    [SerializeField]
    Team blueTeam = null;
    // Red goal post obj를 넣어주기
    [SerializeField]
    Team redTeam = null;
    private void Awake()
    {

    }
    public void SetScore(Team _team, UserInfo _userInfo, int _score)
    {
        if (_team.GetTeamName().Equals(blueTeam.GetTeamName()))
        {
            blueTeam.SetTeamScore(_score);
        }
        if (_team.GetTeamName().Equals(redTeam.GetTeamName()))
        {
            redTeam.SetTeamScore(_score);
        }
        _userInfo.SetInGamePersonalScore(_score);
    }

    public override string ToString()
    {
        return "BlueTeam: " + blueTeam.GetTeamScore() + " RedTeam: " + redTeam.GetTeamScore();
    }
} // end of class
