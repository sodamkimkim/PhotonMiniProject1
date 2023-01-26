using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public string userId { get; set; }
    public string userNicName { get; set; }
    public int nowRank { get; set; }
    // 총 play시간
    public float totPlayTime { get; set; }
    // player 이긴횟수
    public int cntWinning { get; set; }
    // player 진 횟수
    public int cntLose { get; set; }
    // 도움율(tot 개인점수/게임팀점수)
    public int totalScore { get; set; }
} // end of class
