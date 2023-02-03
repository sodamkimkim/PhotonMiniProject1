using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }
    private static UIManager m_instance;
    
    [SerializeField]
    private TextMeshProUGUI redteamScoreTxt;

    [SerializeField]
    private TextMeshProUGUI blueteamScoreTxt;

    [SerializeField]
    private GameObject gameOverUI; // 게임 오버시 활성화할 UI

    [SerializeField]
    private TextMeshProUGUI endTimeTxt;
    [SerializeField]
    private TextMeshProUGUI nowTimeTxt;

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateRedteamScore(int _newScore)
    {
        redteamScoreTxt.text = "" + _newScore;
    }
    public void UpdateBlueteamScore(int _newScore)
    {
        blueteamScoreTxt.text = "" + _newScore;
    }
    public void SetActiveGameOverUI(bool _active)
    {
        gameOverUI.SetActive(_active);
    }
    public void SetNowTime(float _nowTime)
    {
        nowTimeTxt.text = ""+ (int)_nowTime;
    }
    public void SetEndTime(int _endTime)
    {
        endTimeTxt.text = "" + _endTime;
    }
} // end of class
