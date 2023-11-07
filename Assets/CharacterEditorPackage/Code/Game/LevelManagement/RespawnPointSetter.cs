using UnityEngine;
using System.Collections;
//--------------------------------------------------------------------
//When the character enters, the respawn point associated with this trigger will be used as the new startpoint
//--------------------------------------------------------------------
public class RespawnPointSetter : MonoBehaviour {
    [SerializeField] int m_Index = 0;
    private InSceneLevelSwitcher m_LevelSwitcher;

    public void Start()
    {
        m_LevelSwitcher = FindObjectOfType<InSceneLevelSwitcher>();
    }

    void OnTriggerEnter()
    {
        if (m_LevelSwitcher != null)
        {
            m_LevelSwitcher.SetIndex(m_Index);
        }
    }
}
