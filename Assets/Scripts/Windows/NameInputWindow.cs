﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInputWindow : Window
{
    private int numSlots = 5;
    private char[] slots;
    private Text nickname;
    private int currentSlot = 0;

    private void OnEnable()
    {
        ResultsController r = Controller.resultsController;
        r.onStatsOK += createWindow;
        r.onNameInputOK += destroyWindow;

        r.onLetterCycleNext += nextLetter;
        r.onLetterCyclePrev += prevLetter;
        r.onLetterSlotNext += nextSlot;
        r.onLetterSlotPrev += prevSlot;
    }

    private void OnDisable()
    {
        ResultsController r = Controller.resultsController;
        r.onStatsOK -= createWindow;
        r.onNameInputOK -= destroyWindow;

        r.onLetterCycleNext -= nextLetter;
        r.onLetterCyclePrev -= prevLetter;
        r.onLetterSlotNext -= nextSlot;
        r.onLetterSlotPrev -= prevSlot;
    }

    protected override void createWindow()
    {
        base.createWindow();

        slots = new char[numSlots];
        for (int i = 0; i < numSlots; i++)
            slots[i] = ' ';

        nickname = getTextComponent("Nickname");
    }

    private void changeSlotsBy(int by) { currentSlot = Mathf.Clamp(currentSlot + by, 0, numSlots - 1); }
    private void nextSlot() { changeSlotsBy(1); }
    private void prevSlot() { changeSlotsBy(-1); }

    private void changeLetterBy(int by)
    {
        int newASCII = slots[currentSlot] + by;
        print("before: " + newASCII);
        if (newASCII == 64 || newASCII == 91)
            newASCII = 32;
        else if (newASCII == 31)
            newASCII = 90;
        else if (newASCII == 33)
            newASCII = 65;
        slots[currentSlot] = (char)newASCII;
        nickname.text = new string(slots);
        print("after: " + newASCII);
        //print(string.Format("[{0}]nickname: {1}", currentSlot, nickname.text));
    }

    private void nextLetter() { changeLetterBy(1); }
    private void prevLetter() { changeLetterBy(-1); }

    private void uploadName()
    {
        print("doot doot doo " + nickname.text + " got the highscore");
    }
}