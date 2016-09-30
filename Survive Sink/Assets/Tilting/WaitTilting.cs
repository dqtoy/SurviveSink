﻿using UnityEngine;
using System.Collections;

public class WaitTilting : TiltingA {
    private static int MODIFIER = 1;
    private static int WAIT_TIME_RANGE = 2000 / MODIFIER;
    private static int WAIT_TIME_FLOOR = 1000 / MODIFIER;
    private static int WAIT_TIME_BUFFER = 1000 / MODIFIER;

    private int waitTime = 0;
    private int currentMaxWaitTime = 0;

    public override void decideAction()
    {
        if (waitTime > 0)
        {
            waitTime--;
            base.moveToAngle(0,0,WAIT_TIME_RANGE + WAIT_TIME_FLOOR + WAIT_TIME_BUFFER - currentMaxWaitTime + waitTime);
        }
        else
        {
            int temp = base.randomMovement();
            if (temp == 0)
            {
                setRandomWaitTime();
            };
        }
    }

    void setWaitTime(int x)
    {
        waitTime = x;
        currentMaxWaitTime = x;
    }

    void setRandomWaitTime()
    {
        setWaitTime((int)Random.Range(0.0f, 1.0f) * WAIT_TIME_RANGE + WAIT_TIME_FLOOR);
    }
}
