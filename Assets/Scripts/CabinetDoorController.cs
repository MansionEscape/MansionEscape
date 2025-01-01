using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CabinetDoorController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Animator cabinetDoor = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (openTrigger)
            {
                cabinetDoor.Play("CabinetDoorOpen", 0, 0.0f);
                gameObject.SetActive(false);
            }
            else if (closeTrigger)
            {
                {
                    cabinetDoor.Play("CabinetDoorClose", 0, 0.0f);
                    gameObject.SetActive(false);
                }
            }
        }

        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.gameObject == player)
        //    {
        //        if (openTrigger)
        //        {
        //            cabinetDoor.Play("CabinetDoorOpen", 0, 0.0f);
        //            gameObject.SetActive(false);
        //        }
        //    }
        //}
    }
}
