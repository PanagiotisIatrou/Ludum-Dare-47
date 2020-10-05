using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPickUpDrop : MonoBehaviour
{
    // Singleton
    private static TrainPickUpDrop _instance;
    public static TrainPickUpDrop Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TrainPickUpDrop>();
            }

            return _instance;
        }
    }

    public GameObject WagonPrefab;
    private Wagon wagon;
    private TrainMovement trainMovement;
    private int sizeofcargo;
    private int balance;
    List<int> inventory;
    private bool isResupplying = false;

    private void Start()
    {
        wagon = GetComponent<Wagon>();
        trainMovement = GetComponent<TrainMovement>();
        sizeofcargo = 1;
        inventory = new List<int>(sizeofcargo);
        balance = 0;
    }

    public void PickUpDrop()
    {
        // Check if train is in factory
        if ((Vector2)transform.position == Vector2.zero)
        {
            if (TrailDestroyer.Instance.Flage)
            {
                if (TrailDestroyer.IsTrailDestroyed())
                    TrailDestroyer.RestoreTrail();
                TrailDestroyer.DestroyRandomTrail();
            }

            int temp = sizeofcargo - inventory.Count;
            for (int i = 0; i < temp; i++)
            {
                if (Factory.isEmpty())
                    break;
                inventory.Add(Factory.Remove(1)[0]);
            }
        }
        // Check if train is in any city
        //blue
        else if ((Vector2)transform.position == new Vector2(-4, 4))
        {
            remove(0);
        }
        //red
        else if ((Vector2)transform.position == new Vector2(4, 4))
        {
            remove(1);
        }
        //green
        else if ((Vector2)transform.position == new Vector2(-4, -4))
        {
            remove(2);
        }
        //yellow
        else if ((Vector2)transform.position == new Vector2(4, -4))
        {
            remove(3);
        }
        else if ((Vector2)transform.position == new Vector2(0, 2) && !TrailDestroyer.Instance.upVert.state)
        {
            Debug.LogError("GameOver");
        }
        else if ((Vector2)transform.position == new Vector2(-2, 0) && !TrailDestroyer.Instance.leftVert.state)
        {
            Debug.LogError("GameOver");
        }
        else if ((Vector2)transform.position == new Vector2(2, 0) && !TrailDestroyer.Instance.rightVert.state)
        {
            Debug.LogError("GameOver");
        }
        else if ((Vector2)transform.position == new Vector2(0, -2) && !TrailDestroyer.Instance.downVert.state)
        {
            Debug.LogError("GameOver");
        }
    }

    private bool flage = false;

    private void remove(int what)
    {
        listremove(what);
        if (flage)
        {
            balance++;
            if (balance % 2 == 0)
            {
                if(balance % 4 == 0)
                {
                    sizeofcargo++;
                    SpawnWagon();
                    balance = 0;
                }
                Factory.TimeRate();
                Wagon.UpgradeSpeed();
            }

        }
        flage = false;
    }

    private void listremove(int what)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == what)
            {
                ScoreMoneyManager.AddScore(1);
                inventory.RemoveAt(i);
                listremove(what);
                flage = true;
                break;
            }
        }

    }

    private void SpawnWagon()
    {
        // Spawn wagon
        Wagon lastWagon = wagon;
        Wagon temp = wagon;
        while (true)
        {
            temp = temp.previousWagon;
            if (temp == null)
                break;
            lastWagon = temp;
        }

        // Now we have last
        Wagon preLastWagon = lastWagon.frontWagon;
        List<Vertex> neighbours = lastWagon.currentVertex.neighbours;

        Vector2 offset = lastWagon.transform.position - preLastWagon.transform.position;
        Vector2 newPos = (Vector2)lastWagon.transform.position + offset;
        bool isOk = false;
        for (int i = 0; i < neighbours.Count; i++)
        {
            if (newPos == (Vector2)neighbours[i].transform.position)
            {
                isOk = true;
                break;
            }
        }
        if (!isOk)
            newPos = neighbours[0].transform.position;
        // Spawn new wagon on newPos
        GameObject newGO = Instantiate(WagonPrefab, newPos, Quaternion.identity);
        Wagon newWagon = newGO.GetComponent<Wagon>();
        newWagon.frontWagon = lastWagon;
        lastWagon.previousWagon = newWagon;
    }
}
