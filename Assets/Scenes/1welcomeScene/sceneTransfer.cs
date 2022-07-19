using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneTransfer : MonoBehaviour
{
    public void startGame()
    {
        Debug.Log("hello");

        SceneManager.LoadScene("actualGame");


    }

}
