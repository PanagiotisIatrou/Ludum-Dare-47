using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class TrainPickUpDrop : MonoBehaviour
{
    private int sizeofcargo ;
    List<int> inventory;
    private void Start()
    {
        sizeofcargo = 1;
        inventory = new List<int>(sizeofcargo);
    }
    private void Update()
    {
        Debug.Log(inventory.Count);
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
            TrainMovement.UpgradeSpeed();
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
