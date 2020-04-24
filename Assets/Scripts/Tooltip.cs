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

    public string planetName;
    string randomFunFact;
    PlanetInfo planetInfo;

    void Start()
    {
        planetInfo = null;
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

        foreach (PlanetInfo _pInfo in data.planets)
        {
            if (_pInfo.name == planetName.ToLower())
            {
                planetInfo = _pInfo; break;
            }
        }

        if (planetInfo != null)
        {
            descLabel.GetComponent<TextMeshProUGUI>().text = planetInfo.description;
            funFactLabel.GetComponent<TextMeshProUGUI>().text = randomFunFact;
        } else
        {
            Debug.LogError("Failed to load info for " + planetName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (planetInfo != null)
            randomFunFact = planetInfo.fun_facts[Random.Range(0, planetInfo.fun_facts.Count)];

        transform.rotation = Quaternion.LookRotation(transform.position - GameObject.FindGameObjectWithTag("MainCamera").transform.position);
    }
}
