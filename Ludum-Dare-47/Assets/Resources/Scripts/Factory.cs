using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Factory : MonoBehaviour
{
    private const int max_items = 4;
    private int max_time = 2000;
    private int time = 0;
    private Queue<GameObject> items = new Queue<GameObject>(max_items);
    private Queue<int> num_items = new Queue<int>(max_items);
    public GameObject blue;
    public GameObject red;
    public GameObject green;
    public GameObject yellow;

    private void Start()
    {
        AddItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<int> list= Remove(1);
        }

        if (items.Count > max_items)
        {
            Debug.LogWarning("GameOver");
        }

        time++;

        if (time >= max_time)
        {
            time = 0;
            AddItem();
        }

    }
    private void AddItem()
    {
        int temp = Random.Range(0, 3);
        GameObject temp2;
        if (temp == 0)
        {
            num_items.Enqueue(0);
            temp2 = blue;
        }
        else if (temp == 1)
        {
            num_items.Enqueue(1);
            temp2 = red;
        }
        else if (temp == 2)
        {
            num_items.Enqueue(2);
            temp2 = green;
        }
        else
        {
            num_items.Enqueue(3);
            temp2 = yellow;
        }
        Debug.Log(items.Count);
        GameObject item = Instantiate(temp2, transform.position, Quaternion.identity);
        if (items.Count == 0)
        {
            item.transform.position = new Vector3(0.12f, 0.5f, 0);
        }
        else if (items.Count == 1)
        {
            item.transform.position = new Vector3(0.52f, 0.5f, 0);
        }
        else if (items.Count == 2)
        {
            item.transform.position = new Vector3(0.12f, 0.072f, 0);
        }
        else
        {
            item.transform.position = new Vector3(0.52f, 0.072f, 0);
        }
        
        items.Enqueue(item);
        
    }
    
    public List<int> Remove(int size)
    {
        List<int> list = new List<int>(size);
        for (int i = 0; i< size; i++)
        {
            GameObject item = items.Dequeue();
            int temp2 = num_items.Dequeue();
            list.Add(temp2);
            Destroy(item);
        }
        int temp = items.Count;
        Queue<GameObject> temp_queue = new Queue<GameObject>(max_items);
        for (int j = 0; j < temp; j++)
        {
            GameObject item = items.Dequeue();
            if (j == 0)
            {
                item.transform.position = new Vector3(0.12f, 0.5f, 0);
            }
            else if (j == 1)
            {
                item.transform.position = new Vector3(0.52f, 0.5f, 0);
            }
            else
            {
                item.transform.position = new Vector3(0.12f, 0.072f, 0);
            }
            temp_queue.Enqueue(item);
        }
        items = temp_queue;
        return list;
    }
}