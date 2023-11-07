using System;
using UnityEngine;

namespace SliceEmAll.Networking
{
    public class GameLobbyManager : MonoBehaviour
    {
        public Action<GameObject> PlayerSpawned;

        [Header("Player")]
        [SerializeField] private Gameplay.Player.PlayerSpawner _playerSpawner;

        [Header("UI")]
        [SerializeField] private Canvas _lobbyHudCanvas;
        [SerializeField] private GameObject _gameplayHudPrefab;
        [SerializeField] private Transform _uiRoot;

        public void Awake()
        {
            if (Networking.ServerConnector.Instance == null)
            {
                return;
            }

            Networking.ServerConnector.Instance.JoinedRoom += SpawnPlayer;
        }

        private void SpawnPlayer()
        {
            GameObject playerObject = _playerSpawner.SpawnPlayer();

            PlayerSpawned?.Invoke(playerObject);

            //_lobbyHudCanvas.enabled = false;

            SpawnGameplayHud(playerObject);
        }

        private void SpawnGameplayHud(GameObject playerObject)
        {
            GameObject gameplayHudObject = Instantiate(_gameplayHudPrefab, _uiRoot);

            PlayerInput playerInput = playerObject.GetComponent<PlayerInput>();

            UIButtonInputOverride uiInput = gameplayHudObject.GetComponentInChildren<UIButtonInputOverride>();
            if (uiInput != null)
            {
                uiInput.SetPlayerInput(playerInput);
            }

            UIDirectionInputOverride uiDirection = gameplayHudObject.GetComponentInChildren<UIDirectionInputOverride>();
            if (uiDirection != null)
            {
                uiDirection.SetPlayerInput(playerInput);
            }
        }
    }
}
