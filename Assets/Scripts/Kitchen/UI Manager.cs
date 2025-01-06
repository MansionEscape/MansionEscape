using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject playerControl;
    public PlayerManager player;

    public TMP_Text loading;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.FindWithTag("PlayerManager");
        player = playerControl.GetComponent<PlayerManager>();
    }


    public void Back()
    {
        StartCoroutine(LoadMansion());
        loading.text = "Loading Mansion...";
    }
    public IEnumerator LoadMansion()
    {
        yield return new WaitForSeconds(2);
        player.LoadPlayerGame();
        SceneManager.LoadScene("Mansion");
    }


}
