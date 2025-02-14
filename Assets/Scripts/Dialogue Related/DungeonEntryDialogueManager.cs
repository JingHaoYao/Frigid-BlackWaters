﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntryDialogueManager : MonoBehaviour
{
    public string[] storyEntryDialogues;
    public string[] randomEntryDialogues;
    public DialogueUI dialogueUI;
    public GameObject dialogueBlackOverlay;
    public int whatDungeonLevel = 1;
    bool loadedDialogue = false;
    public GameObject mapSymbol, infoPointer;
    Dictionary<string, string> entryDialogueDict = new Dictionary<string, string>();
    MissionManager missionManager;

    private void Awake()
    {
        missionManager = FindObjectOfType<MissionManager>();
        for(int i = 0; i < missionManager.missionIDs.Count; i++)
        {
            entryDialogueDict.Add(missionManager.missionIDs[i], storyEntryDialogues[i]);
        }
    }

    public DialogueSet loadDialogue(string name, bool storyDialogue = false)
    {
        switch (whatDungeonLevel)
        {
            case 1:
                if (storyDialogue == true)
                {
                    return Resources.Load<DialogueSet>("Dialogues/First Dungeon Level/Story Dialogues/" + name);
                }
                else
                {
                    return Resources.Load<DialogueSet>("Dialogues/First Dungeon Level/Random Entry Dungeon Dialogue/" + name);
                }
            case 2:
                if (storyDialogue == true)
                {
                    return Resources.Load<DialogueSet>("Dialogues/Second Dungeon Level/Story Dialogues/" + name);
                }
                else
                {
                    return Resources.Load<DialogueSet>("Dialogues/Second Dungeon Level/Random Dungeon Entry Dialogue/" + name);
                }
            case 3:
                if (storyDialogue == true)
                {
                    return Resources.Load<DialogueSet>("Dialogues/Third Dungeon Level/Story Dialogues/" + name);
                }
                else
                {
                    return Resources.Load<DialogueSet>("Dialogues/Third Dungeon Level/Random Entry Dungeon Dialogue/" + name);
                }
            case 4:
                if (storyDialogue == true)
                {
                    return Resources.Load<DialogueSet>("Dialogues/Fourth Dungeon Level/Story Dialogues/" + name);
                }
                else
                {
                    return Resources.Load<DialogueSet>("Dialogues/Fourth Dungeon Level/Random Dungeon Entry Dialogue/" + name);
                }
        }
        return null;
    }

    public void Initialize()
    {
        try
        {
            loadDungeonDialogue();
        }
        catch
        {
            //Just so the rest of the code runs
        }
        loadedDialogue = true;
        SaveSystem.SaveGame();
    }

    void Update()
    {
        if(MiscData.completedEntryDungeonDialogues.Count == 1 && dialogueUI.gameObject.activeSelf == false && loadedDialogue == true && MiscData.dungeonMapSymbolShown == false)
        {
            mapSymbol.SetActive(true);
            if (infoPointer != null)
            {
                infoPointer.SetActive(true);
            }
            MiscData.dungeonMapSymbolShown = true;
            SaveSystem.SaveGame();
        }
    }

    void loadDungeonDialogue()
    {
        if (!MiscData.completedEntryDungeonDialogues.Contains(entryDialogueDict[MiscData.missionID]))
        {
            dialogueUI.LoadDialogueUI(loadDialogue(entryDialogueDict[MiscData.missionID], true), 0);
        }
        else
        {
            if (Random.Range(1, 5) == 1 && MiscData.enoughRoomsTraversed == true)
            {
                dialogueUI.LoadDialogueUI(loadDialogue(randomEntryDialogues[Random.Range(0, randomEntryDialogues.Length)]), 0);
            }
        }
    }
}
