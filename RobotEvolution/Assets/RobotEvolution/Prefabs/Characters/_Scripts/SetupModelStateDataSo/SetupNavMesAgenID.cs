using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetupNavMesAgenID : MonoBehaviour
{
    private LoadCharacterModelStateDataSO _loadCharacterModelStateDataSO;

    private void Awake()
    {
        _loadCharacterModelStateDataSO = GetComponent<LoadCharacterModelStateDataSO>();
    }

    private void Start()
    {
        SetNavModelSetup();
    }

    private void SetNavModelSetup()
    {
        List<CharacterModelStatsDataSO> characterModelStatsDataSOList = _loadCharacterModelStateDataSO.GetloadedModelStateDataSOList();
        
        bool isFound = false;

        for (int i = 0; i < NavMesh.GetSettingsCount(); i++)
        {
            NavMeshBuildSettings navMeshBuildSettings = NavMesh.GetSettingsByIndex(i);
            string navAgenTypeName = NavMesh.GetSettingsNameFromID(navMeshBuildSettings.agentTypeID);

            for (int a = 0; a < characterModelStatsDataSOList.Count; a++)
            {
                if (navAgenTypeName == characterModelStatsDataSOList[a].TypeModelStateCharacter.ToString())
                {
                    characterModelStatsDataSOList[a].navMeshModelStateID = navMeshBuildSettings.agentTypeID;
                    isFound = true;
                    break;
                }

            }

            if (isFound == false)
                Debug.LogError($"LogError: Not found ID NavMeshAgen with this name: {navAgenTypeName}");
        }

    }
}
