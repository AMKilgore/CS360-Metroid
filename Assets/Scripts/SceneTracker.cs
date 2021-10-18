using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneTracker : MonoBehaviour
{
    public Animator animator;

    private Dictionary<string, string> adjacentScenes = new Dictionary<string, string>();
    private static Dictionary<string, Vector2> spawnPoints = new Dictionary<string, Vector2>();
    private string sceneName;
    private string area;

    private static string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        // Get the scene and use it's path to get the current location
        Scene s = SceneManager.GetActiveScene();
        area = s.path;
        area = area.Split('/')[2];
        sceneName = s.name;
        // Get the names of all adjacent scenes
        GoToMapArea();
        Debug.Log(gameObject.name);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void FadeToScene(string location)
    {
        levelToLoad = location;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(adjacentScenes[levelToLoad]);
    }

    void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = spawnPoints[levelToLoad];
    }

    void GoToMapArea()
    {
        switch(area)
        {
            case "Brinstar":
                FindBrinstarScene();
                break;
            default:
                Debug.Log("Could not find map");
                break;
        }
    }

    void FindBrinstarScene()
    {
        adjacentScenes.Clear();
        spawnPoints.Clear();
        switch (sceneName)
        {
            case "BrinstarEntrance":
                adjacentScenes.Add("rightDoor", "BrinstarSecondRoom");
                spawnPoints.Add("rightDoor", new Vector2(-1.14f, 5.13f));
                break;
            case "BrinstarSecondRoom":
                adjacentScenes.Add("leftDoor", "BrinstarEntrance");
                spawnPoints.Add("leftDoor", new Vector2(73.22f, 28.79f));
                adjacentScenes.Add("topRightDoor", "BrinstarLongHallway");
                spawnPoints.Add("topRightDoor", new Vector2(-4.05f, -0.95f));
                break;
            case "BrinstarLongHallway":
                adjacentScenes.Add("leftDoor", "BrinstarSecondRoom");
                spawnPoints.Add("leftDoor", new Vector2(11.21f, 5.13f));
                break;
            default:
                Debug.Log("There isn't a map to load.");
                break;
        }
    }
}
