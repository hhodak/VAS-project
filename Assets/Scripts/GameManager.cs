using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Material collectColor;
    public Material idleColor;
    public Material killColor;
    public Material runColor;
    public Material endGameColor;

    public float fieldSize;
    float fieldScale;

    int numberOfSpheres;
    int numberOfBoxes;
    int numberOfCoins;
    public bool playStatus;

    public GameObject sphere;
    public GameObject box;
    public GameObject coin;

    public int sphereScore = 0;
    public int boxScore = 0;
    public Text sphereScoreText;
    public Text boxScoreText;
    public Text coinsLeftText;

    public GameObject camera;
    public GameObject panel;
    public Text endMessage;

    private void Awake()
    {
        GenerateField();
        SetNumberOfObjects();
        SpawnObjects();
        UpdateScore();
        SetCamera();
        playStatus = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
    }

    void GenerateField()
    {
        fieldScale = Random.Range(2f, 5f);
        fieldSize = fieldScale * 5;
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(0f, 0f, 0f);
        plane.transform.localScale = new Vector3(fieldScale, 1, fieldScale);
    }

    void SetNumberOfObjects()
    {
        numberOfSpheres = 1;
        numberOfBoxes = int.Parse(Mathf.Floor(fieldScale).ToString());
        numberOfCoins = int.Parse(Mathf.Floor(fieldScale).ToString());
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfSpheres; i++)
        {
            float posX;
            float posZ;
            Vector3 vctr;

            do
            {
                posX = Random.Range(-fieldSize, fieldSize);
                posZ = Random.Range(-fieldSize, fieldSize);
                vctr = new Vector3(posX, 2, posZ);
            } while (Physics.CheckSphere(vctr, 1.5f));

            Instantiate(sphere, vctr, Quaternion.identity);
        }

        for (int i = 0; i < numberOfBoxes; i++)
        {
            float posX;
            float posZ;
            Vector3 vctr;

            do
            {
                posX = Random.Range(-fieldSize, fieldSize);
                posZ = Random.Range(-fieldSize, fieldSize);
                vctr = new Vector3(posX, 2, posZ);
            } while (Physics.CheckSphere(vctr, 1.5f));

            Instantiate(box, vctr, Quaternion.identity);
        }

        for (int i = 0; i < numberOfCoins; i++)
        {
            float posX;
            float posZ;
            Vector3 vctr;

            do
            {
                posX = Random.Range(-fieldSize, fieldSize);
                posZ = Random.Range(-fieldSize, fieldSize);
                vctr = new Vector3(posX, 2, posZ);
            } while (Physics.CheckSphere(vctr, 1.5f));

            Instantiate(coin, vctr, Quaternion.identity);
        }
    }

    public void RemoveBox()
    {
        numberOfBoxes--;
        if (numberOfBoxes == 0)
        {
            playStatus = false;
            ShowMessage();
        }
    }

    public void RemoveCoin(GameObject go)
    {
        numberOfCoins--;

        switch (go.tag)
        {
            case "Sphere":
                sphereScore++;
                break;
            case "Box":
                boxScore++;
                break;
        }
        UpdateScore();

        if (numberOfCoins == 0)
        {
            playStatus = false;
            ShowMessage();
        }
    }

    void UpdateScore()
    {
        sphereScoreText.text = sphereScore.ToString();
        boxScoreText.text = boxScore.ToString();
        coinsLeftText.text = numberOfCoins.ToString();
    }

    void ShowMessage()
    {
        panel.SetActive(true);
        if (numberOfBoxes == 0)
            endMessage.text = "Winner is Sphere because there are no more Boxes left.";
        else if (sphereScore > boxScore)
        {
            endMessage.text = "Winner is Sphere with score " + sphereScore.ToString() + ":" + boxScore.ToString();
        }
        else if (sphereScore < boxScore)
        {
            endMessage.text = "Winner is Box with score " + boxScore.ToString() + ":" + sphereScore.ToString();
        }
        else
        {
            endMessage.text = "The game is tied (" + sphereScore.ToString() + ":" + boxScore.ToString() + ")";
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void SetCamera()
    {
        camera.transform.SetPositionAndRotation(new Vector3(0, fieldScale * 10.0f, 0), camera.transform.rotation);
    }
}
