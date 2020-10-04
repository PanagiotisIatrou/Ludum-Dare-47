using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class IsTriggerd : MonoBehaviour
{
    Wagon current;
    private void Awake()
    {
        current = GetComponent<Wagon>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Wagon next = collision.GetComponent<Wagon>();
       
        if(!(next==current.frontWagon||current.previousWagon==next|| current.frontWagon==next.previousWagon))
            Debug.LogError("GameOver");
    }
}
