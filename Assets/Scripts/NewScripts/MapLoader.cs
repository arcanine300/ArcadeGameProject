using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public int MapsLeft { get { return _availableMapNames.Count; } }

    private List<string> _availableMapNames;

    private readonly string MapNamesLocationString = "Refrences/MapNames";

    public void SetMapNames()
    {
        TextAsset mapNamesTextAsset = Resources.Load(MapNamesLocationString) as TextAsset;
        string mapNamesMessage = string.Empty;
        string[] mapNames = Regex.Split(mapNamesTextAsset.text, "\n|\r|\r\n");

        _availableMapNames = new List<string>();

        foreach (var mapName in mapNames)
        {
            if (!string.IsNullOrWhiteSpace(mapName) && !string.IsNullOrEmpty(mapName))
            {
                _availableMapNames.Add(mapName);
                mapNamesMessage += mapName + "\n";
            }
        }
        Debug.Log($"loaded maps:\n {mapNamesMessage}");
    }

    public string GetRandomMapName()
    {
        if (_availableMapNames == null || _availableMapNames.Count <= 0)
        {
            Debug.LogError("Error: No more map names to select.");
            return string.Empty;
        }
        else
        {
            int index = Random.Range(0, _availableMapNames.Count);
            string name = _availableMapNames[index];

            _availableMapNames.RemoveAt(index);
            return name;
        }
    }
}
