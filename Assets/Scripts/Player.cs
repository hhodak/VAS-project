using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    State state;
    public CollectState collectState = new CollectState();
    public IdleState idleState = new IdleState();
    public KillState killState = new KillState();
    public RunState runState = new RunState();
    public EndGameState endGameState = new EndGameState();

    public float currentSpeed;
    public float normalSpeed;
    public float boostSpeed;
    public int coinsCollected;
    public Renderer renderer;
    public Vector3 targetPosition;
    public GameObject targetObject;
    public bool arrived;
    public bool foundCoin;
    public bool foundPrey;
    public bool foundPredator;

    private void OnEnable()
    {
        currentSpeed = normalSpeed;
        state = idleState;
        coinsCollected = 0;
        arrived = true;
        foundCoin = false;
        foundPrey = false;
        foundPredator = false;
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        state = state.DoState(this);
    }

    public void Move()
    {
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (tag)
        {
            case "Sphere":
                SphereEnteringCheck(other.gameObject);
                break;
            case "Box":
                BoxEnteringCheck(other.gameObject);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (tag)
        {
            case "Sphere":
                SphereExitingCheck(other.gameObject);
                break;
            case "Box":
                BoxExitingCheck(other.gameObject);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Box":
                if (gameObject.tag == "Sphere")
                {
                    foundPrey = false;
                    KillBox(collision.gameObject);
                }
                break;
            case "Coin":
                foundCoin = false;
                CollectCoin(collision.gameObject);
                break;
        }
    }

    void CollectCoin(GameObject go)
    {
        Destroy(go);
        coinsCollected++;
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.RemoveCoin(gameObject);
    }

    void GoTowards(GameObject go)
    {
        targetPosition = go.transform.position;
        arrived = false;
    }

    void GoAway(GameObject go)
    {
        GameObject GM = GameObject.Find("GameManager");
        GameManager gm = GM.GetComponent<GameManager>();

        do
        {
            float posX = Random.Range(-gm.fieldSize, gm.fieldSize);
            float posZ = Random.Range(-gm.fieldSize, gm.fieldSize);

            Vector3 vctr = new Vector3(posX, 0, posZ);
            targetPosition = vctr;
        } while (!Physics.CheckSphere(targetPosition, 3));

        arrived = false;
    }

    void SphereEnteringCheck(GameObject go)
    {
        if (!foundPrey)
        {
            if (go.tag == "Box")
            {
                GoTowards(go);
                foundPrey = true;
                targetObject = go;
            }
            else if (go.tag == "Coin")
            {
                GoTowards(go);
                foundCoin = true;
            }
        }
    }

    void BoxEnteringCheck(GameObject go)
    {
        if (!foundPredator)
        {
            if (go.tag == "Sphere")
            {
                GoAway(go);
                foundPredator = true;
            }
            else if (go.tag == "Coin")
            {
                GoTowards(go);
                foundCoin = true;
            }
        }
    }

    void SphereExitingCheck(GameObject go)
    {
        if (targetObject != null)
            if (go.name == targetObject.name)
            {
                foundPrey = false;
            }
    }

    void BoxExitingCheck(GameObject go)
    {
        if (go.tag == "Sphere")
        {
            foundPredator = false;
        }

    }

    public void Follow()
    {
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, step);
    }

    void KillBox(GameObject go)
    {
        Destroy(go);
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.RemoveBox();
    }
}