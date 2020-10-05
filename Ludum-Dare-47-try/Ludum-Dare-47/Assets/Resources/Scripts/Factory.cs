using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    // Singleton
    private static Factory _instance;
    public static Factory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Factory>();
            }

            return _instance;
        }
    }

    private const int max_items = 4;
    private float max_time = 5;
    private float time = 0;
    private Queue<GameObject> items = new Queue<GameObject>(max_items);
    private Queue<int> num_items = new Queue<int>(max_items);
    private bool flagewanring;
    public GameObject warningpref;
    public GameObject blue;
    public GameObject red;
    public GameObject green;
    public GameObject yellow;
    private GameObject warning;
    public AudioSource warnignsound;

    private void Awake()
    {
        flagewanring = false;
        AddItem();
    }

    void Update()
    {

        if (flagewanring && items.Count != 4)
        {
            Destroy(warning);
            flagewanring = false;
            warnignsound.Stop();
        }

        if (items.Count > max_items)
        {
            TrailDestroyer.Instance.GameOver();
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

        if(Instance.max_time > 2f)
        {
            if (ScoreMoneyManager.HowMuchScore() < 10)
                Instance.max_time -= 0.2f;
            else if (ScoreMoneyManager.HowMuchScore() < 20)
                Instance.max_time -= 0.15f;
            else
                Instance.max_time -= 0.1f;
        }
    }
    private void AddItem()
    {
        int temp = Random.Range(0, 4);
        GameObject temp2;
        if (temp == 0)
        {
            num_items.Enqueue(0);
            temp2 = blue;   //Wood
        }
        else if (temp == 1)
        {
            num_items.Enqueue(1);
            temp2 = red;    //Bread
        }
        else if (temp == 2)
        {
            num_items.Enqueue(2);
            temp2 = green;  //Stone
        }
        else
        {
            num_items.Enqueue(3);
            temp2 = yellow; //Iron
        }
        GameObject item = Instantiate(temp2, transform.position, Quaternion.identity);
        if (items.Count == 0)
        {
            item.transform.position = new Vector3(-0.4083648f, 0.4594103f, 0);
        }
        else if (items.Count == 1)
        {
            item.transform.position = new Vector3(0.4083648f, 0.4594103f, 0);
        }
        else if (items.Count == 2)
        {
            item.transform.position = new Vector3(-0.4083648f, -0.394103f, 0);
        }
        else
        {
            item.transform.position = new Vector3(0.4083648f, -0.394103f, 0);
        }
        
        items.Enqueue(item);

        if (items.Count == 4)
        {
            flagewanring = true;
            warnignsound.Play();
            warning = Instantiate(warningpref, new Vector3(0.0453741f, 0.1417933f, 0), Quaternion.identity);
        }

    }
    
    public static List<int> Remove(int size)
    {
        if (Instance.items.Count == 0)
        {
            return new List<int>(0);
        }
        List<int> list = new List<int>(size);
        for (int i = 0; i< size; i++)
        {
            GameObject item = Instance.items.Dequeue();
            int temp2 = Instance.num_items.Dequeue();
            list.Add(temp2);
            Destroy(item);
        }
        int temp = Instance.items.Count;
        Queue<GameObject> temp_queue = new Queue<GameObject>(max_items);
        for (int j = 0; j < temp; j++)
        {
            GameObject item = Instance.items.Dequeue();
            if (j == 0)
            {
                item.transform.position = new Vector3(-0.4083648f, 0.4594103f, 0);
            }
            else if (j == 1)
            {
                item.transform.position = new Vector3(0.4083648f, 0.4594103f, 0);
            }
            else
            {
                item.transform.position = new Vector3(-0.4083648f, -0.4594103f, 0);
            }
            temp_queue.Enqueue(item);
        }
        Instance.items = temp_queue;
        return list;
    }
    
    public static bool isEmpty()
    {
        if (Instance.items.Count != 0)
            return false;
        return true;
    }
}