using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTeam : Team
{
    string teamName = null;
    List<string> teamMemList = new List<string>();
    int teamScore;

    private void Awake()
    {
        teamName = ScoreManager.eTeamName.RedTeam.ToString();
    }
    public override string GetTeamName()
    {
        return teamName;
    }
    public override List<string> GetTeamMemList()
    {
        return teamMemList;
    }

    public override int GetTeamScore()
    {
        return teamScore;
    }

    public override void SetTeamMemList(string _playerNickName)
    {
        teamMemList.Add(_playerNickName);
    }

    public override void SetTeamScore(int _score)
    {
        teamScore += _score;
    }
}