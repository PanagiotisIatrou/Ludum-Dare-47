using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TrainPickUpDrop : MonoBehaviour
{
    private int sizeofcargo = 2;
    List<int> inventory;
    private void Start()
    {
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
            if (!Factory.isEmpty())
            {
                int temp = sizeofcargo - inventory.Count ;
                if (temp >= 0)
                {
                    for (int i = 0; i < temp; i++)
                    {
                        inventory.Add(Factory.Remove(1)[0]);
                    }
                }
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

    private void remove(int what)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == what)
            {
                ScoreMoneyManager.AddScore(1);
                inventory.RemoveAt(i);
                remove(what);
                break;
            }
        }
        
    }
}
