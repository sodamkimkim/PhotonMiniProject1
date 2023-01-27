using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTeam : Team
{
    string redTeamName = null;
    List<string> redTeamMemList = new List<string>();
    int redTeamScore;

    private void Awake()
    {
        redTeamName = ScoreManager.eTeamName.RedTeam.ToString();
    }
    public override string GetTeamName()
    {
        return redTeamName;
    }
    public override List<string> GetTeamMemList()
    {
        return redTeamMemList;
    }

    public override int GetTeamScore()
    {
        return redTeamScore;
    }

    public override void SetTeamMemList(string _playerNickName)
    {
        redTeamMemList.Add(_playerNickName);
    }

    public override void SetTeamScore(int _score)
    {
        redTeamScore += _score;
    }
}