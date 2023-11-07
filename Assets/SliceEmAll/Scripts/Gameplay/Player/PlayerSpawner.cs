using Photon.Pun;
using UnityEngine;

namespace SliceEmAll.Gameplay.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;

        public GameObject SpawnPlayer()
        {
            return PhotonNetwork.Instantiate(playerPrefab.name, transform.position, transform.rotation);
        }
    }
}
