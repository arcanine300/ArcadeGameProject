using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class Refrences : MonoBehaviour
{
    public static Refrences Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        GetDictionaries();
    }

    public Dictionary<string, List<string>> Weapons { get; private set; }

    public Dictionary<string, List<string>> Defenses { get; private set; }

    public Dictionary<string, List<string>> Items { get; private set; }

    public void GetDictionaries()
    {
        Weapons = Add(Resources.Load("Refrences/Weapons") as TextAsset);
        Defenses = Add(Resources.Load("Refrences/Defenses") as TextAsset);
        Items = Add(Resources.Load("Refrences/Items") as TextAsset);
    }

    private Dictionary<string, List<string>> Add(TextAsset textAsset)
    {
        string data = textAsset.text;

        string[] lines = Regex.Split(data, "\n|\r|\r\n");

        var temp = new Dictionary<string, List<string>>
        {
            { "green", new List<string>() },
            { "blue", new List<string>() },
            { "purple", new List<string>() },
            { "red", new List<string>() }
        };

        for (int i = 0; i < lines.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(lines[i]) && !string.IsNullOrEmpty(lines[i]))
            {
                lines[i] = lines[i].Replace(" ", string.Empty);
                string[] parts = lines[i].Split(':');
                temp[parts[0]].Add(parts[1]);
            }
        }

        List<List<string>> temps = Enumerable.ToList(temp.Values);

        return temp;
    }

    //private Dictionary<string, List<string>> AddStringToStringDict(TextAsset textAsset)
    //{
    //    string data = textAsset.text;

    //    string[] lines = Regex.Split(data, "\n|\r|\r\n");

    //    Dictionary<string, string> temp = new Dictionary<string, string>();

    //    for (int i = 0; i < lines.Length; i++)
    //    {
    //        if (!string.IsNullOrWhiteSpace(lines[i]) && !string.IsNullOrEmpty(lines[i]))
    //        {
    //            lines[i] = lines[i].Replace(" ", string.Empty);
    //            string[] parts = lines[i].Split(':');

    //            if (!temp.ContainsKey(parts[0]))
    //            {
    //                temp.Add(parts[0], parts[1]);
    //            }
    //        }
    //    }

    //    return temp;
    //}

    public string GetRandomWeapon(string rarity)
    {
        if (Weapons.ContainsKey(rarity))
        {
            if (Weapons[rarity].Count > 0)
            {
                int i = Random.Range(0, Weapons[rarity].Count);
                return Weapons[rarity][i];
            }           
        }

        return string.Empty;
    }

    public string GetRandomDefense(string rarity)
    {
        if (Defenses.ContainsKey(rarity))
        {
            if (Defenses[rarity].Count > 0)
            {
                int i = Random.Range(0, Defenses[rarity].Count);
                return Defenses[rarity][i];
            }
        }

        return string.Empty;
    }

    public string GetRandomItem(string rarity)
    {
        if (Items.ContainsKey(rarity))
        {
            if (Items[rarity].Count > 0)
            {
                int i = Random.Range(0, Items[rarity].Count);
                return Items[rarity][i];
            }
        }

        return string.Empty;
    }
}
