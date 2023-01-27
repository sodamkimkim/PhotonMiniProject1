using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTeam : Team
{
    string blueTeamName = null;
    List<string> blueTeamMemList = new List<string>();
    int blueTeamScore;

    private void Awake()
    {
        blueTeamName = ScoreManager.eTeamName.BlueTeam.ToString();
    }
    public override string GetTeamName()
    {
        return blueTeamName;
    }

    public override List<string> GetTeamMemList()
    {
        return blueTeamMemList;
    }
    public override int GetTeamScore()
    {
        return blueTeamScore;
    }

    public override void SetTeamMemList(string _playerNickName)
    {
        blueTeamMemList.Add(_playerNickName);
    }

    public override void SetTeamScore(int _score)
    {
        blueTeamScore += _score;
    }


}
