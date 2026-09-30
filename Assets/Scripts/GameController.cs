using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class GameController : MonoBehaviour
{

    public ButtonControl buttonControl;
    public PipeControl pipeControl;
    [SerializeField] Transform XRRig;
    [SerializeField] Transform StartLocation;
    [SerializeField] Transform GameLocation;

    [SerializeField] TextMeshProUGUI Text1;
    [SerializeField] TextMeshProUGUI Text2;
    [SerializeField] TextMeshProUGUI Text3;
    [SerializeField] TextMeshProUGUI YesText;
    [SerializeField] TextMeshProUGUI NoText;
    [SerializeField] TextMeshProUGUI WinText;
    [SerializeField] TextMeshProUGUI LoseText;
    [SerializeField] TextMeshProUGUI TimerText;

    [SerializeField] GameObject YesButton;
    [SerializeField] GameObject YesButtonBack;
    [SerializeField] GameObject NoButton;
    [SerializeField] GameObject NoButtonBack;

    int timeLeft;
    bool win;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        XRRig.position = StartLocation.position;
        XRRig.rotation = StartLocation.rotation;
        timeLeft = 61;
        win = false;
        WinText.enabled = false;
        LoseText.enabled = false;
        TimerText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        win = buttonControl.ButtonsSolved && pipeControl.PipesSolved;
    }

    void winScreen() {
        clearStartRoom();
        WinText.enabled = true;
        XRRig.position = StartLocation.position;
        XRRig.rotation = StartLocation.rotation;
    }

    void loseScreen() {
        clearStartRoom();
        LoseText.enabled = true;
        XRRig.position = StartLocation.position;
        XRRig.rotation = StartLocation.rotation;
    }

    void gameStart() {
        XRRig.position = GameLocation.position;
        StartCoroutine(gameTimer());
        TimerText.enabled = true;
    }

    IEnumerator gameTimer() {
        while(timeLeft > 0 && !win) {
            timeLeft--;
            yield return new WaitForSeconds(1f);
            TimerText.text = timeLeft.ToString();
        }
        if(win) winScreen();
        else loseScreen();      
    }

    public void yesPressed(SelectEnterEventArgs args) {
        gameStart();
    }

    public void noPressed(SelectEnterEventArgs args) {
        loseScreen();
    }

    void clearStartRoom() {
        Text1.enabled = false;
        Text2.enabled = false;
        Text3.enabled = false;
        YesText.enabled = false;
        NoText.enabled = false;
        TimerText.enabled = false;
        YesButton.SetActive(false);
        YesButtonBack.SetActive(false);
        NoButton.SetActive(false);
        NoButtonBack.SetActive(false);
            }
}
