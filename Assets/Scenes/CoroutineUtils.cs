using System;
using System.Collections;
using UnityEngine;

public class CoroutineUtils
{
    public static IEnumerator ExecuteAction(Action action)
    {
        action();
        yield return null;
    }

    public static IEnumerator ExecuteWhenConditionIsTrue(Func<bool> condition, Action action)
    {
        while (!condition())
        {
            yield return null;
        }
        action();
    }

    public static IEnumerator ExecuteAfterDelayInFrames(int delayInFrames, Action action)
    {
        for (int i = 0; i < delayInFrames; i++)
        {
            yield return null;
        }
        // Code to execute after the delay
        action();
    }

    public static IEnumerator ExecuteAfterDelayInSeconds(float delayInSeconds, Action action)
    {
        yield return new WaitForSeconds(delayInSeconds);
        // Code to execute after the delay
        action();
    }

    public static IEnumerator ExecuteRepeatedlyInSeconds(float delayInSeconds, Action action)
    {
        while (true)
        {
            action();
            yield return new WaitForSeconds(delayInSeconds);
        }
    }
}