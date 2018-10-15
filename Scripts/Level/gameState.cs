using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class gameState : MonoBehaviour {
    public float gameTime;
    [SerializeField]
    GameObject player;
    public enum GameStates { Wave, Upgrading, Dead, MainMenu, Pregame, Paused };
    public enum LevelTypes { Arcade, Tutorial };
    public LevelTypes levelType;
    public GameStates playerState;
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    GameObject rightHand;
    [SerializeField]
    EnemySpawner EnemyManager;
    [SerializeField]
    InterfaceAnimManager UpgradeScreen;
    [SerializeField]
    InterfaceAnimManager GameOverScreen;
    [SerializeField]
    GameObject keyBoard;
    [SerializeField]
    LetterTyper name;
    [SerializeField]
    List<GameObject> MainMenuObjects = new List<GameObject>();
    [SerializeField]
    List<GameObject> DisableOnDeath = new List<GameObject>();
    [SerializeField]
    List<GameObject> MoveTutorialObjects = new List<GameObject>();
    [SerializeField]
    List<GameObject> SpinTutorialObjects = new List<GameObject>();
    [SerializeField]
    List<GameObject> GunTutorialObjects = new List<GameObject>();

    void Update()
    {
        if (playerState == GameStates.Wave)
            gameTime += Time.deltaTime;
    }

    public void StartSpinTutorial()
    {
        for (int i =0; i < MoveTutorialObjects.Count; i ++)
        {
            MoveTutorialObjects[i].SetActive(false);
            if (MoveTutorialObjects[i].GetComponent<g_UIToggleActive>())
                MoveTutorialObjects[i].GetComponent<g_UIToggleActive>().active = false;
        }
        for (int i =0; i < SpinTutorialObjects.Count; i ++)
        {
            SpinTutorialObjects[i].SetActive(true);
            if (SpinTutorialObjects[i].GetComponent<g_UIToggleActive>())
                SpinTutorialObjects[i].GetComponent<g_UIToggleActive>().active = true;
        }
    }

    public void StartGunTutorial()
    {
        for (int i = 0; i < SpinTutorialObjects.Count; i++)
        {
            SpinTutorialObjects[i].SetActive(false);
            if (SpinTutorialObjects[i].GetComponent<g_UIToggleActive>())
                SpinTutorialObjects[i].GetComponent<g_UIToggleActive>().active = false;
        }
        for (int i = 0; i < GunTutorialObjects.Count; i++)
        {
            GunTutorialObjects[i].SetActive(true);
            if (GunTutorialObjects[i].GetComponent<g_UIToggleActive>())
                GunTutorialObjects[i].GetComponent<g_UIToggleActive>().active = true;
        }
    }

    public void CloseGunTutorial()
    {
        GunTutorialObjects[0].SetActive(false);
    }

    //Button Activated
    public void StartNextWave()
    {
        //Starts coroutine so that wait seconds can be used
        StartCoroutine(NextWave());
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(.5f);
        playerState = GameStates.Wave;
        StartCoroutine(EnemyManager.StartNextWave());
        UpgradeScreen.startDisappear();
    }

    public void StartUpgrading()
    {
        //change playerstate and popup the upgrade screen
        playerState = GameStates.Upgrading;
        UpgradeScreen.startAppear();
    }

    public void MainMenu(int x)
    {
        for (int i =0; i < MainMenuObjects.Count; i++)
        {
            if (i == x)
                MainMenuObjects[i].SetActive(true);
            else
                MainMenuObjects[i].SetActive(false);
        }
    }

    public void SetDegrees(int degrees)
    {
        PlayerPrefs.SetInt("Degrees", degrees);
    }

    public void KillPlayer()
    {
        EnemyManager.DeleteAllEnemies();
        playerState = GameStates.Dead;
        if (levelType == LevelTypes.Arcade)
        {
            for (int i = 0; i <DisableOnDeath.Count; i++)
            {
                DisableOnDeath[i].SetActive(false);
            }
            GameOverScreen.startAppear();
        }
    }

    public void KeyBoardScreen()
    {
        //delete gameover screen and popup keyboard
        Destroy(GameOverScreen.gameObject);
        keyBoard.SetActive(true);
    }

    public void EndLevel()
    {
        //save score and go to main menu
        if (levelType == LevelTypes.Arcade)
        {
            GetComponent<SaveScoreToFile>().AddScore(player.GetComponent<scoreTracker>().score, name.text.text);
            GetComponent<SaveScoreToFile>().SaveScore();
        }
        LoadScene(0);
    }

    public void LoadScene(int scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
