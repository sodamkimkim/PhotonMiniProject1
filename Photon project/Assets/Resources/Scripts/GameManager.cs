using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    private static GameManager m_instance;
    [SerializeField]
    private GameObject bluePlayerPrefab;
    [SerializeField]
    private GameObject redPlayerPrefab;
    private int redScore = 0;
    private int blueScore = 0;
    private const int gameEndRunTime = 300;
    private float gameNowtime;
    private Vector3 redPlayerOriginPos = new Vector3(-0.33f, 8.33f, -6.35f);
    private Vector3 bluePlayerOriginPos = new Vector3(0.33f, 8.33f, 6.35f);
    private bool isGameover = false;
    private List<GameObject> playerGoList = new List<GameObject>();

    private int playerIdx;
    private void Awake()
    {
        UIManager.instance.SetEndTime(gameEndRunTime);
        if (instance != this)
        {
            Destroy(gameObject);
        }
        gameNowtime = 0f;
        playerIdx = 0;
    }
    private void Start()
    {
        CreatePlayer();
    }
    private void CreatePlayer()
    {
        if (bluePlayerPrefab != null && redPlayerPrefab != null)
        {
            Vector3 playerPos = new Vector3(0, 0, 0);
            GameObject playerGo = null;

            if (playerIdx % 2 == 0)
            { // ÇöÀç ¹æ ¸â¹ö Â¦¼ö¸é ºí·çÆÀÀ¸·Î »ý¼º
                playerGo = PhotonNetwork.Instantiate(bluePlayerPrefab.name, bluePlayerOriginPos, Quaternion.identity);
                photonView.RPC("ApplyPlayerIdx", RpcTarget.All);
            }
            else
            { // ÇöÀç ¹æ ¸â¹ö È¦¼ö¸é ·¹µåÆÀÀ¸·Î »ý¼º
                playerGo = PhotonNetwork.Instantiate(redPlayerPrefab.name, redPlayerOriginPos, Quaternion.identity);
                photonView.RPC("ApplyPlayerIdx", RpcTarget.All);
            }
        }
    }
    [PunRPC]
    public void ApplyPlayerIdx()
    {
        playerIdx += 1;
    }
    [PunRPC]
    public void ApplyPlayerList()
    {
        int playerCnt = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCnt == playerGoList.Count) return;
        Debug.LogError("CurrentRoom PlayerCount: " + playerCnt);
        PhotonView[] photonViews = FindObjectsOfType<PhotonView>();
        playerGoList.Clear();
        for (int i = 0; i < playerCnt; ++i)
        {
            int key = i + 1;
            for (int j = 0; j < photonViews.Length; ++j)
            {
                if (photonViews[j].isRuntimeInstantiated == false) continue;
                if (PhotonNetwork.CurrentRoom.Players.ContainsKey(key) == false) continue;

                int viewNum = photonViews[j].Owner.ActorNumber;
                int playerNum = PhotonNetwork.CurrentRoom.Players[key].ActorNumber;
                if (viewNum == playerNum)
                {
                    photonViews[j].gameObject.name = "Player_" + photonViews[j].Owner.NickName;
                    playerGoList.Add(photonViews[j].gameObject);
                }
            }
        }
    }

    private void Update()
    {
        if (!isGameover)
        {
            gameNowtime += Time.deltaTime;
            UIManager.instance.SetNowTime(gameNowtime);

            if (gameNowtime >= gameEndRunTime)
            {
                gameNowtime = gameEndRunTime;
                UIManager.instance.SetNowTime(gameNowtime);

                EndGame();
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }
    }
    private void EndGame()
    {
        isGameover = true;
        UIManager.instance.SetActiveGameOverUI(true);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(redScore);
            stream.SendNext(blueScore);
            stream.SendNext(gameNowtime);
            stream.SendNext(playerIdx);

        }
        else
        {
            redScore = (int)stream.ReceiveNext();
            blueScore = (int)stream.ReceiveNext();
            gameNowtime = (float)stream.ReceiveNext();
            playerIdx = (int)stream.ReceiveNext();

            UIManager.instance.SetNowTime(gameNowtime);
        }
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
} // end of class
