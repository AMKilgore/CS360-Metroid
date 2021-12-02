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
    public string sceneName;
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
        GameObject.Find("Player").GetComponent<Transform>().position = spawnPoints[levelToLoad];
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
        string musicName = "Brinstar";
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
                adjacentScenes.Add("rightDoor", "BrinstarTower");
                spawnPoints.Add("rightDoor", new Vector2(-0.5f, -54.86f));
                break;
            case "BrinstarTower":
                adjacentScenes.Add("bottomLeftDoor", "BrinstarLongHallway");
                spawnPoints.Add("bottomLeftDoor", new Vector2(56.04f, -0.95f));
                adjacentScenes.Add("middleLeftDoor", "LongBeamHallway");
                spawnPoints.Add("middleLeftDoor", new Vector2(56.09f, -0.95f));
                adjacentScenes.Add("topLeftDoor", "UpperLongHallway");
                spawnPoints.Add("topLeftDoor", new Vector2(56.09f, -0.95f));
                break;
            case "LongBeamHallway":
                musicName = "ItemRoom";
                adjacentScenes.Add("rightDoor", "BrinstarTower");
                spawnPoints.Add("rightDoor", new Vector2(-0.96f, 80.06f));
                adjacentScenes.Add("leftDoor", "LongBeamRoom");
                spawnPoints.Add("leftDoor", new Vector2(56.09f, -0.95f));
                break;
            case "LongBeamRoom":
                musicName = "ItemRoom";
                adjacentScenes.Add("rightDoor", "LongBeamHallway");
                spawnPoints.Add("rightDoor", new Vector2(3.88f, -0.95f));
                break;
            case "UpperLongHallway":
                adjacentScenes.Add("rightDoor", "BrinstarTower");
                spawnPoints.Add("rightDoor", new Vector2(-0.96f, 125f));
                break;
            default:
                Debug.Log("There isn't a map to load.");
                break;
        }
        // Change audio if needed
        if (GameObject.Find("Music").GetComponent<AudioSource>().clip.name != musicName)
            GameObject.Find("Music").GetComponent<MusicManager>().ChangeMusic(musicName);
        // Save player info
        FindObjectOfType<PlayerMovement>().SavePlayer();
    }
}
