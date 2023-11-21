using System.Linq;
using Photon.Pun;
using UnityEngine;

namespace SliceEmAll.Gameplay.Networking
{
    public class NetworkCharacterColorSelector : MonoBehaviourPun
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color[] _colors;

        public void Awake()
        {
            if (_colors.Count() > photonView.ViewID - 1)
            {
                _spriteRenderer.color = _colors[photonView.ViewID - 1];
            }

            _spriteRenderer.sortingOrder = photonView.IsMine ? 0 : 1;
        }
    }
}