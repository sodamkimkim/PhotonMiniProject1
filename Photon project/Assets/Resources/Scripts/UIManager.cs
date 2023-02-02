using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Text redteamScoreTxt;

    [SerializeField]
    private Text blueteamScoreTxt;

    [SerializeField]
    private GameObject gameOverUI; // 게임 오버시 활성화할 UI

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
    public void SetActiveGameOberUI(bool _active)
    {
        gameOverUI.SetActive(_active);
    }
} // end of class
