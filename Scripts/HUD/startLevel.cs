using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class startLevel : MonoBehaviour
{
    [SerializeField]
    int level;
    
    void StartLevel()
    {
        SceneManager.LoadScene(level);
    }
}
