using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PlanetInfo
{
    public string name;
    public string description;
    public List<string> fun_facts;
}

[System.Serializable]
public class PlanetJSON
{
    public PlanetInfo[] planets;
}

public class Tooltip : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject planetLabel;
    public GameObject descLabel;
    public GameObject funFactLabel;

    string planetName;

    void Start()
    {
        
    }

    public void SetPlanetName(string input)
    {
        planetName = input;
        planetLabel.GetComponent<TextMeshProUGUI>().text = input;
        LoadInfo();
    }

    void LoadInfo()
    {
        PlanetJSON data = JsonUtility.FromJson<PlanetJSON>(GameObject.Find("Manager").GetComponent<Manager>().planetInfoJSON.text);
        PlanetInfo pInfo = null;

        foreach (PlanetInfo _pInfo in data.planets)
        {
            if (_pInfo.name == planetName.ToLower())
            {
                pInfo = _pInfo; break;
            }
        }

        if (pInfo != null)
        {
            descLabel.GetComponent<TextMeshProUGUI>().text = pInfo.description;
            funFactLabel.GetComponent<TextMeshProUGUI>().text = pInfo.fun_facts[Random.Range(0, pInfo.fun_facts.Count)];
        } else
        {
            Debug.LogError("Failed to load info for " + planetName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - GameObject.FindGameObjectWithTag("MainCamera").transform.position);
    }
}
