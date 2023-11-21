using Destructible2D.Examples;
using Photon.Pun;
using UnityEngine;

namespace SliceEmAll.Gameplay.Enemy
{
    public class EnemySpawner : MonoBehaviourPun
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private InSceneLevelSwitcher _levelSwitcher;
        [SerializeField] private Vector2 _waypointOffset;

        public void OnEnable()
        {
            _levelSwitcher.OnLevelStart += Spawn;
        }

        private void Spawn()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            _levelSwitcher.OnLevelStart -= Spawn;

            GameObject enemyObject = SpawnEnemy();

            if (enemyObject.TryGetComponent<D2dWaypoints>(out D2dWaypoints d2DWaypoints))
            {
                d2DWaypoints.PointOffset = new Vector2(transform.position.x, transform.position.y) + _waypointOffset;
            }
        }

        public GameObject SpawnEnemy()
        {
            GameObject enemyObject = PhotonNetwork.Instantiate(_enemyPrefab.name, transform.position, transform.rotation);
            enemyObject.name = gameObject.name;
            return enemyObject;
        }
    }
}