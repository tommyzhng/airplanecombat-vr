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
    public GameObject instructionPanel;

    //Plane Selection
    public GameObject podium;
    private GameObject item;
    public List<GameObject> planeList;
    private int planeIndex = 0;

    private void Start()
    {
        planeSelection.enabled = false;
    }
    //When start menu clicked
    public void startGame()
    {
        startScreen.enabled = false;
        planeSelection.enabled = true;

        StartCoroutine(MoveIn(podium, new Vector3(-4.5f, 0f, 0f)));
        StartCoroutine(MoveIn(planeList[planeIndex], new Vector3(-4.5f, 1.25f, 0f)));
    }
    //Next Plane
    public void NextPlane()
    {
        StopAllCoroutines();
        StartCoroutine(MoveOut(planeList[planeIndex]));
        planeIndex = (planeIndex + 1) % planeList.Count;
        StartCoroutine(MoveIn(planeList[planeIndex], new Vector3(-4.5f, 1.25f, 0f)));

    }
    //Previous Plane
    public void PrevPlane()
    {
        StopAllCoroutines();
        StartCoroutine(MoveOut(planeList[planeIndex]));
        if (planeIndex == 0)
        {
            planeIndex = planeList.Count - 1;
        }
        else
        {
            planeIndex--;
        }
        StartCoroutine(MoveIn(planeList[planeIndex], new Vector3(-4.5f, 1.25f, 0f)));
    }
    //Action of animating in the plane
    public IEnumerator MoveIn(GameObject item, Vector3 dest)
    {
        float movePoint = 0f;
        while (item.transform.position != dest)
        {
            item.transform.position = Vector3.Lerp(item.transform.position, dest, movePoint += .25f * Time.deltaTime);
            yield return null;
        }
        movePoint = 0f;
    }
    //Action of animating out the plane
    public IEnumerator MoveOut(GameObject item)
    {
        Vector3 dest = new Vector3(-31.24f, 0f, 6.75f);
        float move = 0f;
        while (item.transform.position != dest)
        {
            item.transform.position = Vector3.Lerp(item.transform.position, dest, move += .25f * Time.deltaTime);
            yield return null;
        }
        move = 0f;
    }


    public void Instructions()
    {
        StopAllCoroutines();
        StartCoroutine(MoveIn(instructionPanel, new Vector3(-6.71f, 1.4f, -8.98f)));
    }
    public void InstructionsOut()
    {
        StopAllCoroutines();
        StartCoroutine(MoveIn(instructionPanel, new Vector3(-6.71f, -4f, -8.98f)));
    }
    

    //When confirm button clicked
    public void SpawnTutorial()
    {
        PlayerPrefs.SetInt("selectedPlane", planeIndex);
        SceneManager.LoadSceneAsync("tutorial");
        
    }

}
