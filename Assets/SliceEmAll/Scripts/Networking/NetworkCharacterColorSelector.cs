using Photon.Pun;
using UnityEngine;

namespace SliceEmAll.Gameplay.Networking
{
    public class NetworkCharacterColorSelector : MonoBehaviourPun
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _localPlayerColor;
        [SerializeField] private Color _networkPlayerColor;

        public void Awake()
        {
            _spriteRenderer.color = photonView.IsMine ? _localPlayerColor : _networkPlayerColor;
            _spriteRenderer.sortingOrder = photonView.IsMine ? 0 : 1;
        }
    }
}