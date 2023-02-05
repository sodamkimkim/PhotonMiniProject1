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
    private int gameEndRunTime = 500;
    private float gameNowtime;
    private Vector3 redPlayerOriginPos = new Vector3(-0.33f, 8.33f, -6.35f);
    private Vector3 bluePlayerOriginPos = new Vector3(0.33f, 8.33f, 6.35f);
    private bool isGameover = false;
    private List<GameObject> playerGoList = new List<GameObject>();

    private int playerIdx;
    private void Awake()
    {

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
        gameEndRunTime = 500;
    }
    private void CreatePlayer()
    {
        if (bluePlayerPrefab != null && redPlayerPrefab != null)
        {
            Vector3 playerPos = new Vector3(0, 0, 0);
            GameObject playerGo = null;
            int playerCnt = PhotonNetwork.CurrentRoom.PlayerCount;
            if (playerCnt % 2 == 1)
            { 
                playerGo = PhotonNetwork.Instantiate(bluePlayerPrefab.name, bluePlayerOriginPos, Quaternion.identity);
                playerGo.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
               photonView.RPC("ApplyPlayerList", RpcTarget.All);
                UIManager.instance.SetNickName(playerGo.GetPhotonView().name);

                Debug.LogError("2:" + playerGo.name);
            }
            else
            { 
                playerGo = PhotonNetwork.Instantiate(redPlayerPrefab.name, redPlayerOriginPos, Quaternion.identity);
                //playerIdx++;
                photonView.RPC("ApplyPlayerList", RpcTarget.All);
                //if(PhotonNetwork.IsMasterClient)
                {
                UIManager.instance.SetNickName(playerGo.GetPhotonView().name);

                }

                Debug.LogError("1:" + playerGo.name);
            }
        }
    }

    [PunRPC]
    public void ApplyPlayerIdx(int _playerIdx)
    {
        playerIdx = _playerIdx;
           UIManager.instance.SetplayerIdx(playerIdx);
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
            UIManager.instance.SetEndTime(gameEndRunTime);
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

        if(PhotonNetwork.IsMasterClient)
        {
            UIManager.instance.SetplayerIdx(playerGoList.Count);
        }
        else
        {
            UIManager.instance.SetplayerIdx(playerIdx);

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
            stream.SendNext(gameEndRunTime);
            stream.SendNext(redScore);
            stream.SendNext(blueScore);
            stream.SendNext(gameNowtime);
            stream.SendNext(playerGoList.Count);

        }
        else
        {
            gameEndRunTime = (int)stream.ReceiveNext();
            redScore = (int)stream.ReceiveNext();
            blueScore = (int)stream.ReceiveNext();
            gameNowtime = (float)stream.ReceiveNext();
            playerIdx = (int)stream.ReceiveNext();

            UIManager.instance.SetEndTime(gameEndRunTime);
            UIManager.instance.SetNowTime(gameNowtime);
          //  UIManager.instance.SetplayerIdx(playerIdx);

        }
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
} // end of class
