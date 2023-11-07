using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace SliceEmAll.Networking
{
    public class ServerConnector : MonoBehaviourPunCallbacks
    {
        private static ServerConnector _instance;

        public static ServerConnector Instance { get => _instance; private set => _instance = value; }

        private System.Random _random;
        public Action JoinedRoom;
        public Action<Photon.Realtime.Player> PlayerEnteredRoom;
        public Action<Photon.Realtime.Player> PlayerExitedRoom;

        private List<Photon.Realtime.Player> _playersInLobby = new List<Photon.Realtime.Player>();

        public void Init()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = this;
            _random = new System.Random();
        }

        public void ConnectToServer()
        {
            Debug.Log("Connecting to server.");
            PhotonNetwork.GameVersion = Application.version;
            PhotonNetwork.NickName = $"Slicer{_random.Next(10000)}";
            PhotonNetwork.ConnectUsingSettings();
        }

        public void CreateOrJoinRoom()
        {
            if (!PhotonNetwork.IsConnected)
            {
                return;
            }

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.BroadcastPropsChangeToAll = true;
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.JoinOrCreateRoom("basic", roomOptions, TypedLobby.Default);
        }

        public List<Photon.Realtime.Player> GetAllPlayersInLobby()
        {
            return _playersInLobby;
        }


#region PhotonCallbacks
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to server.");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log($"Disconnected from server for reason {cause}.");
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Created room successfully.");
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log($"Room creation failed: {message}.");
        }

        public override void OnJoinedRoom()
        {
            _playersInLobby = PhotonNetwork.CurrentRoom.Players.Values.ToList();
            JoinedRoom?.Invoke();
        }

        public override void OnLeftRoom()
        {
            OnPlayerLeftRoom(PhotonNetwork.LocalPlayer);
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            if (_playersInLobby.Any(x => x.UserId == newPlayer.UserId))
            {
                return;
            }

            _playersInLobby.Add(newPlayer);
            PlayerEnteredRoom?.Invoke(newPlayer);

            Debug.Log($"Player {newPlayer.NickName} entered room.");
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            int idx = _playersInLobby.FindIndex(x => x.UserId == otherPlayer.UserId);
            
            if (idx == -1)
            {
                return;
            }

            _playersInLobby.RemoveAt(idx);
            PlayerExitedRoom?.Invoke(otherPlayer);

            Debug.Log($"Player {otherPlayer.NickName} left room.");
        }
#endregion
    }
}


