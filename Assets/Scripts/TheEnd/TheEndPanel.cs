using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEndPanel : MonoBehaviour
{
    //Call from animation
    private void LoadCreditsScene()
    {
        SceneManager.LoadScene(2);
    }
}
