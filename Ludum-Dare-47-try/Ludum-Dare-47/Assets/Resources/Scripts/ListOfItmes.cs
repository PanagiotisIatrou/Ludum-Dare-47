using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListOfItmes : MonoBehaviour
{
    // Singleton
    private static ListOfItmes _instance;
    public static ListOfItmes Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ListOfItmes>();
            }

            return _instance;
        }
    }

    public TextMeshProUGUI wood;
    public TextMeshProUGUI stone;
    public TextMeshProUGUI bread;
    public TextMeshProUGUI iron;

    public List<int> item = new List<int>(4);

    private void Start()
    {
        for (int i = 0; i < 4; i++)
            item.Add(0);
    }
    public void Check()
    {

        wood.SetText(item[0].ToString() + "x");
        stone.SetText(item[1].ToString() + "x");
        bread.SetText(item[2].ToString() + "x");
        iron.SetText(item[3].ToString() + "x");
    }
}
