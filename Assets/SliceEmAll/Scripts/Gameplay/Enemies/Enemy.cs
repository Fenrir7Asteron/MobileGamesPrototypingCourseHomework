using System;
using UnityEngine;

namespace SliceEmAll.Gameplay.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private InSceneLevelSwitcher _levelSwitcher;

        public void OnDeath()
        {
            AbilityModuleManager[] abilityModuleManagers = FindObjectsOfType<AbilityModuleManager>();
            foreach (var abilityModuleManager in abilityModuleManagers)
            {
                if (!abilityModuleManager.photonView.IsMine)
                {
                    continue;
                }

                abilityModuleManager.GetModuleWithName("DoubleJump")
                    .InitModule(abilityModuleManager.GetCharacterControler());
            }
        }

        public void SetLevelSwitcher(InSceneLevelSwitcher levelSwitcher)
        {
            _levelSwitcher = levelSwitcher;
        }

        public void RespawnPlayer()
        {
            _levelSwitcher.Respawn();
        }
    }
}