using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PipeControl : MonoBehaviour
{
    [SerializeField] GameObject Pipe1;
    [SerializeField] GameObject Pipe2;
    [SerializeField] GameObject Pipe3;
    [SerializeField] GameObject Pipe4;
    [SerializeField] GameObject Pipe5;
    [SerializeField] GameObject Pipe6;
    [SerializeField] GameObject Pipe7;
    [SerializeField] GameObject Pipe8;
    [SerializeField] GameObject Pipe9;

    GameObject[] Pipes = new GameObject[9];
    public bool PipesSolved;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillArrays();
        randomizeDirection();
        PipesSolved = isSolved();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PipesSolved) PipesSolved = isSolved();
    }
    
    void fillArrays() {
        Pipes[0] = Pipe1;
        Pipes[1] = Pipe2;
        Pipes[2] = Pipe3;
        Pipes[3] = Pipe4;
        Pipes[4] = Pipe5;
        Pipes[5] = Pipe6;
        Pipes[6] = Pipe7;
        Pipes[7] = Pipe8;
        Pipes[8] = Pipe9;
    }

    void randomizeDirection() {
        foreach(GameObject Pipe in Pipes) {
            int num = Random.Range(0, 4);
            Vector3 current = Pipe.transform.eulerAngles;
            Pipe.transform.rotation = Quaternion.Euler(current.x, current.y, (current.z + (num * 90)));
        }
    }

    public void onClick(SelectEnterEventArgs args) {
        if(!PipesSolved) {
            var clickedTile = args.interactableObject as UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable;
            int i = System.Array.IndexOf(Pipes, clickedTile.gameObject);
            Pipes[i].transform.Rotate(0f, 0f, 90f);
            // Debug.Log(PipesSolved ? "PipesSolved" : "Unsolved");
        }
    }

    bool isSolved() {
        for(int i = 0; i < Pipes.Length; i++) {
            float r = Pipes[i].transform.eulerAngles.z;
            int rotation = (int)Mathf.Round(r);
            rotation /= 90;
            if(i % 3 != 1) {
                if(rotation != 0) {
                    // Debug.Log(i + " Is Incorrect");
                    return false;
                }
            } else {
                if(rotation % 2 != 0) {
                    // Debug.Log(i + " is incorrect");
                    return false;
                }
            }
        }
        // Debug.Log("Correct!");
        return true;
    }
}
