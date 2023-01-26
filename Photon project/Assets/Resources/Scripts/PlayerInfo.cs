using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo :  UserInfo
{
    public enum Team { BlueTeam, RedTeam};
    public Team team { get; set; }
    public int personalScore { get; set; }
    public int teamTotalScore { get; set; }
} // end of class
