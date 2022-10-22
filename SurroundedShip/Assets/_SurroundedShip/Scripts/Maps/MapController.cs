using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public Image panel;

    public Map currentMap;
    public int CurrentMapId => currentMap.mapId;

    public Map[] possibleMapPrefabs;

    private bool isInitialized;

    void Start()
    {
        if (currentMap != null) return;

        LoadMap();
    }

    public void ShowMapScreen()
    {
        panel.gameObject.SetActive(true);

        if (currentMap == null) LoadMap();
    }

    public void HideMapScreen()
    {
        panel.gameObject.SetActive(false);
    }

    private void LoadMap()
    {
        int newMapId = OptionsHolder.instance.save.currentMap;
        Map chosenMap = null;

        if (newMapId == 0)
        {
            Debug.Log("Choosing new random map!");
            chosenMap = GetRandomMap();
        }
        else
        {
            foreach (Map i in possibleMapPrefabs)
            {
                if (i.mapId == newMapId) chosenMap = i;
                break;
            }
        }

        chosenMap = chosenMap != null ? chosenMap : possibleMapPrefabs[0];

        currentMap = Instantiate(chosenMap, Vector3.zero, Quaternion.identity, panel.transform);
    }

    private Map GetRandomMap()
    {
        return possibleMapPrefabs[Random.Range(0, possibleMapPrefabs.Length)];
    }
}
