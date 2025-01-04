using UnityEngine;

public class SwitchToggle : MonoBehaviour
{
    private Animator switchAnimator;
    public GameObject Light;

    private void Awake()
    {
        // Get Animator component attached to this GameObject
        switchAnimator = GetComponent<Animator>();

        // If IsOn parameter is false, automatically set light to false
        if (!switchAnimator.GetBool("IsOn"))
        {
            Light.SetActive(false);
        }
    }

    private void Update()
    {
        // Check for input
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleSwitch();
        }
    }

    private void OnMouseDown()
    {
        // Toggle switch on mouse click
        ToggleSwitch();
    }

    private void ToggleSwitch()
    {
        // Retrieve current value of IsOn parameter
        bool isOn = switchAnimator.GetBool("IsOn");

        // Toggle IsOn parameter
        switchAnimator.SetBool("IsOn", !isOn);

        // Toggle Light based on IsOn value
        if (!isOn)
        {
            Light.SetActive(true);
        }
        else
        {
            Light.SetActive(false);
        }
    }
}
