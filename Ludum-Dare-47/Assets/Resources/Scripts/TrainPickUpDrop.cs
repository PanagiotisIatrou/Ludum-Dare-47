﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class TrainPickUpDrop : MonoBehaviour
{
    public GameObject WagonPrefab;
    private Wagon wagon;
    private int sizeofcargo;
    List<int> inventory;

    private void Start()
    {
        wagon = GetComponent<Wagon>();
        sizeofcargo = 1;
        inventory = new List<int>(sizeofcargo);
    }

    public void PickUpDrop()
    {
        // Check if train is in factory
        if ((Vector2)transform.position == Vector2.zero)
        {

            if (TrailDestroyer.IsTrailDestroyed())
                TrailDestroyer.RestoreTrail();
            TrailDestroyer.DestroyRandomTrail();
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
    }

    private bool flage = false;

    private void remove(int what)
    {
        listremove(what);
        if (flage)
        {
            sizeofcargo++;
            Factory.TimeRate();
            Wagon.UpgradeSpeed();

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
}