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
    private GameObject item;
    public List<GameObject> planeList;
    private int planeIndex = 0;

    public Button next;
    public Button prev;

    private void Start()
    {
        planeSelection.enabled = false;

    }
    //Podium Functions
    public void NextPlane()
    {
        StopAllCoroutines();
        StartCoroutine(MoveOut(planeList[planeIndex]));
        planeIndex = (planeIndex + 1) % planeList.Count;
        StartCoroutine(MoveIn(planeList[planeIndex], new Vector3(-4.5f, 1.25f, 0f)));

    }
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

    //When start button is clicked
    public void startGame()
    {
        startScreen.enabled = false;
        planeSelection.enabled = true;

        StartCoroutine(MoveIn(podium, new Vector3(-4.5f, 0f, 0f)));
        StartCoroutine(MoveIn(planeList[planeIndex], new Vector3(-4.5f, 1.25f, 0f)));
    }

    //When tutorial clicked
    public void SpawnTutorial()
    {
        SceneManager.LoadScene("tutorial");
    }

}
