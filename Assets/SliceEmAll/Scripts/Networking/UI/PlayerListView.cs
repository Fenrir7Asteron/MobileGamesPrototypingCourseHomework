using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace SliceEmAll.Networking.UI
{
    public class PlayerListView : MonoBehaviour
    {
        [SerializeField] private PlayerInfoElementView _playerInfoElementView;
        [SerializeField] private Transform _content;
        List<PlayerInfoElementView> _playerViewsCreated = new List<PlayerInfoElementView>();

        public void Start()
        {
            ServerConnector instance = Networking.ServerConnector.Instance;
            if (instance == null)
            {
                return;
            }

            instance.PlayerEnteredRoom += OnPlayerEnteredRoom;
            instance.PlayerExitedRoom += OnPlayerLeftRoom;
            instance.JoinedRoom += RefreshPlayerList;
        }

        public void RefreshPlayerList()
        {
            if (!PhotonNetwork.IsConnected)
            {
                return;
            }

            ServerConnector instance = Networking.ServerConnector.Instance;
            if (instance == null)
            {
                return;
            }

            // Destroy old player views
            foreach (var player in _playerViewsCreated)
            {
                Destroy(player.gameObject);
            }
            _playerViewsCreated.Clear();

            // Add new player views for the actual players in lobby
            Player[] playersInLobby = PhotonNetwork.PlayerList;
            foreach (var player in playersInLobby)
            {
                OnPlayerEnteredRoom(player);
            }

            Debug.Log("Refreshed player list");
        }

        public void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            Debug.Log($"Player id {newPlayer.UserId}, nickname {newPlayer.NickName} entered room");

            if (_playerViewsCreated.Any(x => x.GetUserID() == newPlayer.UserId))
            {
                return;
            }

            PlayerInfoElementView playerInfoElementView = Instantiate(_playerInfoElementView, _content);
            playerInfoElementView.SetPlayerInfo(newPlayer);
            _playerViewsCreated.Add(playerInfoElementView);
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            int idx = _playerViewsCreated.FindIndex(x => x.GetUserID() == otherPlayer.UserId);
            
            if (idx == -1)
            {
                return;
            }

            Destroy(_playerViewsCreated[idx].gameObject);
            _playerViewsCreated.RemoveAt(idx);
        }
    }
}
