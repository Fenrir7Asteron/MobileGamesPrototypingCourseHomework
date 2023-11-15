using Photon.Pun;
using SliceEmAll.Networking.UI;
using UnityEngine;

namespace SliceEmAll.Gameplay.Player
{
    public class Player : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerNameElementView playerNameCanvasPrefab;
        [SerializeField] private Transform playerNameTargetPosition;
        private PlayerNameElementView _playerNameElementView;

        public void Start()
        {
            _playerNameElementView = Instantiate(playerNameCanvasPrefab,
                playerNameTargetPosition.position, playerNameTargetPosition.rotation);

            _playerNameElementView.SetTargetPosition(playerNameTargetPosition);
            _playerNameElementView.SetPlayerName(photonView.Owner.NickName);
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            Destroy(gameObject);
        }

        public void OnDestroy()
        {
            if (_playerNameElementView != null)
            {
                Destroy(_playerNameElementView.gameObject);
            }
        }
    }
}
