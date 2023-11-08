using Photon.Pun;
using SliceEmAll.Networking.UI;
using UnityEngine;

namespace SliceEmAll.Gameplay.Player
{
    public class Player : MonoBehaviourPun
    {
        [SerializeField] private PlayerNameElementView playerNameCanvasPrefab;
        [SerializeField] private Transform playerNameTargetPosition;

        public void Start()
        {
            PlayerNameElementView playerNameElementView = Instantiate(playerNameCanvasPrefab,
                playerNameTargetPosition.position, playerNameTargetPosition.rotation);

            playerNameElementView.SetTargetPosition(playerNameTargetPosition);
            playerNameElementView.SetPlayerName(photonView.Owner.NickName);
        }
    }
}
