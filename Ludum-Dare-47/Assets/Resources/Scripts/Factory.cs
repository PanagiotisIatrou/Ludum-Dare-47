using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Factory : MonoBehaviour
{
    private const int max_items = 4;
    private static float max_time = 5;
    private float time = 0;
    private static Queue<GameObject> items = new Queue<GameObject>(max_items);
    private static Queue<int> num_items = new Queue<int>(max_items);
    public GameObject blue;
    public GameObject red;
    public GameObject green;
    public GameObject yellow;

    private void Awake()
    {
        AddItem();
    }

    void Update()
    {

        if (items.Count > max_items)
        {
            Debug.LogWarning("GameOver");
        }

        time += Time.deltaTime;
        if (time >= max_time)
        {
            time = 0;
            AddItem();
        }

    }
    public static void TimeRate()
    {

        if(max_time >2f)
            if (ScoreMoneyManager.HowMuchScore() < 10)
                max_time -= 0.2f;
            else if (ScoreMoneyManager.HowMuchScore() < 20)
                max_time -= 0.15f;
            else
                max_time -= 0.1f;
    }
    private void AddItem()
    {
        int temp = Random.Range(0, 4);
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
    
    public static List<int> Remove(int size)
    {
        if (items.Count == 0)
        {
            return new List<int>(0);
        }
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
    
    public static bool isEmpty()
    {
        if (items.Count != 0)
            return false;
        return true;
    }
}