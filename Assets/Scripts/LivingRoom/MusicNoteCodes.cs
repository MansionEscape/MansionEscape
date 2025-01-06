using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicNoteCodes : MonoBehaviour
{
    public GameObject playerControl;
    public PlayerManager player;

    public AudioSource C_Note;
    public AudioSource D_Note;
    public AudioSource E_Note;
    public AudioSource F_Note;
    public AudioSource G_Note;
    public AudioSource A_Note;
    public AudioSource B_Note;
    public AudioSource C1_Note;
    public AudioSource D1_Note;
    public AudioSource E1_Note;
    public AudioSource F1_Note;
    public AudioSource CS_Note;
    public AudioSource DS_Note;
    public AudioSource FS_Note;
    public AudioSource GS_Note;
    public AudioSource Bb_Note;
    public AudioSource CS1_Note;
    public AudioSource DS1_Note;

    // Puzzle completion variables
    public GameObject puzzleCompleteImage;
    public TMP_Text puzzleCompleted;
    private List<string> inputSequence = new List<string>();
    private string[] correctSequence = { "D1", "F", "E", "FS", "Bb", "G" }; // Correct sequence of notes

    private void Start()
    {
        playerControl = GameObject.FindWithTag("PlayerManager");
        player = playerControl.GetComponent<PlayerManager>();
        puzzleCompleteImage.SetActive(false); // Ensure puzzle complete image is initially hidden
    }

    public void C_Note_Play()
    {
        PlayNoteAndCheck("C", C_Note);
    }

    public void D_Note_Play()
    {
        PlayNoteAndCheck("D", D_Note);
    }

    public void E_Note_Play()
    {
        PlayNoteAndCheck("E", E_Note);
    }

    public void F_Note_Play()
    {
        PlayNoteAndCheck("F", F_Note);
    }

    public void G_Note_Play()
    {
        PlayNoteAndCheck("G", G_Note);
    }

    public void A_Note_Play()
    {
        PlayNoteAndCheck("A", A_Note);
    }

    public void B_Note_Play()
    {
        PlayNoteAndCheck("B", B_Note);
    }

    public void C1_Note_Play()
    {
        PlayNoteAndCheck("C1", C1_Note);
    }

    public void D1_Note_Play()
    {
        PlayNoteAndCheck("D1", D1_Note);
    }

    public void E1_Note_Play()
    {
        PlayNoteAndCheck("E1", E1_Note);
    }

    public void F1_Note_Play()
    {
        PlayNoteAndCheck("F1", F1_Note);
    }

    public void CS_Note_Play()
    {
        PlayNoteAndCheck("CS", CS_Note);
    }

    public void DS_Note_Play()
    {
        PlayNoteAndCheck("DS", DS_Note);
    }

    public void FS_Note_Play()
    {
        PlayNoteAndCheck("FS", FS_Note);
    }

    public void GS_Note_Play()
    {
        PlayNoteAndCheck("GS", GS_Note);
    }

    public void Bb_Note_Play()
    {
        PlayNoteAndCheck("Bb", Bb_Note);
    }

    public void CS1_Note_Play()
    {
        PlayNoteAndCheck("CS1", CS1_Note);
    }

    public void DS1_Note_Play()
    {
        PlayNoteAndCheck("DS1", DS1_Note);
    }
    public IEnumerator LoadMansion()
    {
        yield return new WaitForSeconds(2);
        player.LoadPlayerGame();
        SceneManager.LoadScene("Mansion");
    }

    private void PlayNoteAndCheck(string note, AudioSource audioSource)
    {
        audioSource.Play();

        // Add the note to the input sequence
        inputSequence.Add(note);

        // Check if the sequence matches the correct sequence
        if (inputSequence.Count > correctSequence.Length ||
            inputSequence[inputSequence.Count - 1] != correctSequence[inputSequence.Count - 1])
        {
            // Reset if the sequence is incorrect
            inputSequence.Clear();
        }
        else if (inputSequence.Count == correctSequence.Length)
        {
            // Display Puzzle Complete image if the sequence is correct
            puzzleCompleteImage.SetActive(true);
            player.data.ObjectivePuzzleTwoComplete = true;
            player.UpdatePlayer();
            puzzleCompleted.text = "Loading Mansion...";
            StartCoroutine(LoadMansion());
            Debug.Log("Puzzle Solved!");
        }
    }
}
