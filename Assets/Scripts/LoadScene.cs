using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private AssetBundle sceneAssets;
    private string[] scenePaths;

    // Start is called before the first frame update
    void Start()
    {
        sceneAssets = AssetBundle.LoadFromFile("Assets/Scenes");
        scenePaths = sceneAssets.GetAllScenePaths();
    }

    public void LoadSceneFromName(string name, string side)
    {
        SceneManager.LoadScene(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
