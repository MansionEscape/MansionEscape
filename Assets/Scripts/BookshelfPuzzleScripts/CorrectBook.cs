using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CorrectBook : MonoBehaviour
{
    public GameObject book;
    public Material defaultMaterial;
    public Material correctMaterial;
    private int count;

    private void Start()
    {
       count = 0;
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == book)
        {
            book.GetComponent<Renderer>().material = correctMaterial;
            count++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == book)
        {
            book.GetComponent<Renderer>().material = defaultMaterial;
        }
    }
}
