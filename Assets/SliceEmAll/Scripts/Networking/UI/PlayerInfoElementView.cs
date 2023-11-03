using System;
using TMPro;
using UnityEngine;

namespace SliceEmAll.Networking.UI
{
    public class PlayerInfoElementView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerName;
        private string _userId;

        public void SetPlayerInfo(in Photon.Realtime.Player playerInfo)
        {
            string localPlayerMark = playerInfo.IsLocal ? "(YOU)" : "";
            _playerName.text = $"{playerInfo.NickName} {localPlayerMark}";
            _userId = playerInfo.UserId;
        }

        public string GetUserID()
        {
            return _userId;
        }
    }
}
