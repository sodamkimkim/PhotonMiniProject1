using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    private static GameManager m_instance;
    public GameObject playerPrefab;
    private int redScore = 0;
    private int blueScore = 0;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(redScore);
            stream.SendNext(blueScore);
        }
        else
        {
            redScore = (int)stream.ReceiveNext();
            blueScore = (int)stream.ReceiveNext();
            UIManager.instanace.UpdateScoreText(redScore);
            UIManager.instanace.UpdateScoreText(blueScore);
        }
    }
} // end of class
