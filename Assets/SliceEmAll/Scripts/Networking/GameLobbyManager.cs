using System;
using Destructible2D.Examples;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SliceEmAll.Networking
{
    public class GameLobbyManager : MonoBehaviour
    {
        public Action<GameObject> PlayerSpawned;

        [Header("Player")]
        [SerializeField] private Gameplay.Player.PlayerSpawner _playerSpawner;

        [Header("UI")]
        [SerializeField] private GameObject _joinButton;
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

            _joinButton.SetActive(false);

            PlayerInput playerInput = playerObject.GetComponent<PlayerInput>();
            playerObject.GetComponent<D2dSpaceshipJumper>().SetPlayerInput(playerInput);

            SpawnGameplayHud(playerInput);

            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, SceneManager.GetActiveScene().name);
        }

        private void SpawnGameplayHud(PlayerInput playerInput)
        {
            GameObject gameplayHudObject = Instantiate(_gameplayHudPrefab, _uiRoot);

            UIButtonInputOverride[] uiInputs = gameplayHudObject.GetComponentsInChildren<UIButtonInputOverride>();
            foreach (UIButtonInputOverride uiInput in uiInputs)
            {
                uiInput.SetPlayerInput(playerInput);
            }
            
            UIDirectionInputOverride[] uiDirections = gameplayHudObject.GetComponentsInChildren<UIDirectionInputOverride>();
            foreach (UIDirectionInputOverride uiDirection in uiDirections)
            {
                uiDirection.SetPlayerInput(playerInput);
            }
        }
    }
}
