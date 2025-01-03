using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlacementManager : MonoBehaviour
{
    public GameObject mainCourse; // The main dish prefab to spawn
    public Transform mainCoursePosition; // The position to place the main dish
    private HashSet<string> placedObjects = new HashSet<string>(); // Track placed objects


    public void RegisterPlacement(string objectTag)
    {
        placedObjects.Add(objectTag);
        Debug.Log($"{objectTag} placed. Total placed: {placedObjects.Count}/12");

        // Check if all objects are placed (i.e., all unique object tags)
        if (placedObjects.Count == 12)
        {
            ShowMainCourse();
        }
    }

    private void ShowMainCourse()
    {
        if (mainCourse != null && mainCoursePosition != null)
        {
            Instantiate(mainCourse, mainCoursePosition.position, Quaternion.identity);
            mainCourse.SetActive(true);
            Debug.Log("Main course placed on the table!");
            
        }
    }

}