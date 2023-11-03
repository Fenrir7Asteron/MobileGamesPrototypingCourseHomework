using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace SliceEmAll.Networking.UI
{
    public class JoinRoomButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        public void Awake()
        {
            button.onClick.AddListener(JoinRoom);  
        }

        private void JoinRoom()
        {
            if (!PhotonNetwork.IsConnected 
                || PhotonNetwork.InRoom
                || Networking.ServerConnector.Instance == null
                )
            {
                return;
            }

            Networking.ServerConnector.Instance.CreateOrJoinRoom();
        }
    }
}
