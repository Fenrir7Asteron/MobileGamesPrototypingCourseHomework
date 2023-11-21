using Photon.Pun;
using UnityEngine;

namespace SliceEmAll.Gameplay.LevelObjects
{
    public class LevelObjectSpawner : MonoBehaviourPun
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private InSceneLevelSwitcher _levelSwitcher;

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

            GameObject enemyObject = SpawnObject();
        }

        public GameObject SpawnObject()
        {
            GameObject spawnedObject = PhotonNetwork.Instantiate(_enemyPrefab.name, transform.position, transform.rotation);
            spawnedObject.transform.localScale = transform.localScale;
            return spawnedObject;
        }
    }
}