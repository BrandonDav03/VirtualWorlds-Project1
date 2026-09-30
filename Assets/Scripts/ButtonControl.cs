using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonControl : MonoBehaviour
{

    [SerializeField] GameObject Button1;
    [SerializeField] GameObject Button2;
    [SerializeField] GameObject Button3;
    [SerializeField] GameObject Button4;
    [SerializeField] GameObject Button5;
    [SerializeField] GameObject Button6;
    [SerializeField] GameObject Button7;
    [SerializeField] GameObject Button8;
    [SerializeField] GameObject Button9;
    [SerializeField] GameObject YesButton;
    [SerializeField] GameObject NoButton;
    [SerializeField] GameObject ButtonBack1;
    [SerializeField] GameObject ButtonBack2;
    [SerializeField] GameObject ButtonBack3;
    [SerializeField] GameObject ButtonBack4;
    [SerializeField] GameObject ButtonBack5;
    [SerializeField] GameObject ButtonBack6;
    [SerializeField] GameObject ButtonBack7;
    [SerializeField] GameObject ButtonBack8;
    [SerializeField] GameObject ButtonBack9;
    [SerializeField] GameObject YesButtonBack;
    [SerializeField] GameObject NoButtonBack;
    GameObject[] Buttons = new GameObject[11];
    GameObject[] ButtonBacks = new GameObject[11];

    [SerializeField] Material Red;
    [SerializeField] Material White;
    [SerializeField] Material Black;

    public bool ButtonsSolved;
    int correct;
    int[] randomNumbers = new int[5];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillArrays();
        ButtonsSolved = false;
        correct = 0;
        for (int i = 0; i < randomNumbers.Length; i++)
        {
            randomNumbers[i] = Random.Range(0, 9);
        }
        StartCoroutine(lightUp());
    }

    // Update is called once per frame
    void Update()
    {
        if(correct == 5) {
            ButtonsSolved = true;
            isButtonsSolved();
        }
    }

    void fillArrays() {
        Buttons[0] = Button1;
        Buttons[1] = Button2;
        Buttons[2] = Button3;
        Buttons[3] = Button4;
        Buttons[4] = Button5;
        Buttons[5] = Button6;
        Buttons[6] = Button7;
        Buttons[7] = Button8;
        Buttons[8] = Button9;
        Buttons[9] = YesButton;
        Buttons[10] = NoButton;

        ButtonBacks[0] = ButtonBack1;
        ButtonBacks[1] = ButtonBack2;
        ButtonBacks[2] = ButtonBack3;
        ButtonBacks[3] = ButtonBack4;
        ButtonBacks[4] = ButtonBack5;
        ButtonBacks[5] = ButtonBack6;
        ButtonBacks[6] = ButtonBack7;
        ButtonBacks[7] = ButtonBack8;
        ButtonBacks[8] = ButtonBack9;
        ButtonBacks[9] = YesButtonBack;
        ButtonBacks[10] = NoButtonBack;
    }

    IEnumerator lightUp() {
        while(!ButtonsSolved) { 
            for(int i = 0; i < randomNumbers.Length; i++) {
                if(ButtonsSolved){isButtonsSolved(); break;}
                Renderer rend = Buttons[randomNumbers[i]].GetComponent<Renderer>();
                rend.material = White;
                yield return new WaitForSeconds(0.4f);
                rend.material = Red;
                yield return new WaitForSeconds(0.2f);
            }
            if(ButtonsSolved){isButtonsSolved(); break;}
            yield return new WaitForSeconds(0.5f);
            if(ButtonsSolved){isButtonsSolved(); break;}
            for(int i = 0; i < 9; i++) {
                Renderer rend = Buttons[i].GetComponent<Renderer>();
                rend.material = White;
            }
            yield return new WaitForSeconds(0.5f);
            if(ButtonsSolved){isButtonsSolved(); break;}
            for(int i = 0; i < 9; i++) {
                Renderer rend = Buttons[i].GetComponent<Renderer>();
                rend.material = Red;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void isButtonsSolved() {
        foreach (GameObject Button in Buttons) {
            Renderer rend = Button.GetComponent<Renderer>();
            rend.material = White;
        }
        foreach (GameObject ButtonBack in ButtonBacks) {
            Renderer rend = ButtonBack.GetComponent<Renderer>();
            rend.material = Black;
        }
    }

    public void changeToWhite(HoverEnterEventArgs args) {
        // Debug.Log("CHANGE COLOR WAS CALLED!");
        if(!ButtonsSolved) {
            var hoveredButton = args.interactableObject as UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable;

            if (hoveredButton == null)
            {
                Debug.LogWarning("Hovered object is not an XRSimpleInteractable.");
                return;
            }

            int i = System.Array.IndexOf(Buttons, hoveredButton.gameObject);

            if (i == -1)
            {
                Debug.LogWarning(
                    "Could not find " + hoveredButton.gameObject.name +
                    " in the Buttons array."
                );
                return;
            }

            // Debug.Log("Hovered button: " + hoveredButton.gameObject.name);
            // Debug.Log("Array index: " + i);

            Renderer rend = ButtonBacks[i].GetComponent<Renderer>();
            rend.material = White;
        }       
    }

    public void changeToBlack(HoverExitEventArgs args) {
       // Debug.Log("CHANGE COLOR WAS CALLED!");
       if(!ButtonsSolved) {
            var hoveredButton = args.interactableObject as UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable;

            if (hoveredButton == null)
            {
                Debug.LogWarning("Hovered object is not an XRSimpleInteractable.");
                return;
            }

            int i = System.Array.IndexOf(Buttons, hoveredButton.gameObject);

            if (i == -1)
            {
                Debug.LogWarning(
                    "Could not find " + hoveredButton.gameObject.name +
                    " in the Buttons array."
                );
                return;
            }

            // Debug.Log("Hovered button: " + hoveredButton.gameObject.name);
            // Debug.Log("Array index: " + i);

            Renderer rend = ButtonBacks[i].GetComponent<Renderer>();
            rend.material = Black;
       }
    }

    public void press(SelectEnterEventArgs args) {
        if(!ButtonsSolved) {
            var pressedButton = args.interactableObject as UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable;
            int i = System.Array.IndexOf(Buttons, pressedButton.gameObject);
            if(i == randomNumbers[correct]) {
                Debug.Log("Correct!");
                correct++;
            } else {
                Debug.Log("Incorrect!");
                correct = 0;
            }
        }
    }
}
