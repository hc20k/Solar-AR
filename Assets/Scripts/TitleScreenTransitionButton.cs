using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreenTransitionButton : MonoBehaviour
{
    public string sceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(transitionToScene);
    }

    void transitionToScene()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = "Loading...";
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
