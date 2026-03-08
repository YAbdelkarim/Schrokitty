using UnityEngine;

public class TutorialDirector : MonoBehaviour
{

    private MasterSuperposition master;
    private int tutorialStage = 0;
    public DialogueController[] dialogueControllers;
    public ObjectSuperposition test;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        master = GameObject.FindGameObjectWithTag("Master").GetComponent<MasterSuperposition>();
    }

    // Update is called once per frame
    void Update()
    {
        if(master.superpositionMode == true && tutorialStage == 0)
        {
            dialogueControllers[tutorialStage].gameObject.SetActive(false);
            dialogueControllers[++tutorialStage].gameObject.SetActive(true);
        }
        else if (master.superpositionMode == false && tutorialStage == 1 && test.currentState == 1)
        {
            dialogueControllers[tutorialStage].gameObject.SetActive(false);
            dialogueControllers[++tutorialStage].gameObject.SetActive(true);
        }
    }
}
