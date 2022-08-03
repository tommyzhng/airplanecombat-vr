using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneTransfer : MonoBehaviour
{
    //All canvas
    public Canvas startScreen;
    public Canvas planeSelection;

    //Plane Selection
    public GameObject podium;
    public List<GameObject> planeList;
    private Vector3 podiumDest = new Vector3(-4.5f, 0f, 0f);
    float movePoint = 0f;
    private void Start()
    {
        planeSelection.enabled = false;
    }
    private void FixedUpdate()
    {
        Podium();
    }
    //Podium Functions
    private void Podium()
    {
        if (planeSelection.enabled && movePoint < 1)
        {
            movePoint += 0.25f * Time.deltaTime;
            podium.transform.position = Vector3.Lerp(podium.transform.position, podiumDest, movePoint);
        }
    }
    //When start button is clicked
    public void startGame()
    {
        startScreen.enabled = false;
        planeSelection.enabled = true;
    }
    public void SpawnTutorial()
    {
        SceneManager.LoadScene("tutorial");
    }

}
