using Photon.Pun;
using TMPro;
using UnityEngine;

namespace SliceEmAll.Networking.UI
{
    public class PlayerNameElementView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerName;
        private Transform _targetPosition;
        
        public void LateUpdate() {
            Vector3 offset = _targetPosition.position - transform.position;
            if (offset.sqrMagnitude > 0.001f)
            {
                transform.position += offset * 0.25f;
            }
        }

        public void SetPlayerName(string nickName)
        {   
            _playerName.text = $"{nickName}";
        }

        public void SetTargetPosition(Transform targetPosition)
        {
            _targetPosition = targetPosition;
        }
    }
}
