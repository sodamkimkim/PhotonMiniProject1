using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Team :MonoBehaviour
{

    public abstract string GetTeamName();
    public abstract List<string> GetTeamMemList();
    public abstract int GetTeamScore();
    public abstract void SetTeamMemList(string _playerNickName);
    public abstract void SetTeamScore(int _score);

} // end of class
