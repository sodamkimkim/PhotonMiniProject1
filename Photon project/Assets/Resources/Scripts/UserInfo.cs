using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public string userId { get; set; }
    public string userNicName { get; set; }
    public int nowRank { get; set; }
    // �� play�ð�
    public float totPlayTime { get; set; }
    // player �̱�Ƚ��
    public int cntWinning { get; set; }
    // player �� Ƚ��
    public int cntLose { get; set; }
    // ������(tot ��������/����������)
    public int totalScore { get; set; }
} // end of class
