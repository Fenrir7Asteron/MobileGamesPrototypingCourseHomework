using UnityEngine;
using System.Collections;
//--------------------------------------------------------------------
//Class used on triggers which signify the end of a level. The InSceneLevelSwitcher will move the player to the next level
//--------------------------------------------------------------------
public class LevelTransition : MonoBehaviour {
    
    [SerializeField] InSceneLevelSwitcher m_LevelSwitcher;
    [SerializeField] int m_Index = 0;

    void OnTriggerEnter()
    {
        if (m_LevelSwitcher != null)
        {
            m_LevelSwitcher.SetIndex(m_Index);
            m_LevelSwitcher.Respawn();
        }
    }
}
