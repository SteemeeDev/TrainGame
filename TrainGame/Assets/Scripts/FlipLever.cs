using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FlipLever : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] TrainTrackMover trackMover;

    // 1 = right turn
    // 0 = left turn

    int currenttrack;

    static Dictionary<int[], int> tracksLookup = new Dictionary<int[], int>(){
        {new int[]{1},       1},
        {new int[]{1,1},     2},
        {new int[]{1,0},     3},
        {new int[]{1,0,0},   4},
        {new int[]{1,0,1},   107},

        {new int[]{0,     }, 100},
        {new int[]{0,1,   }, 101},
        {new int[]{0,0,   }, 102},
        {new int[]{0,0,1, }, 103},
        {new int[]{0,0,0, }, 104},
        {new int[]{0,0,0,0}, 105},
        {new int[]{0,0,1,0}, 105},
        {new int[]{0,0,1,1}, 107},
        {new int[]{0,0,0,1}, 107}
    };
    public List<int> leverPulls = new List<int>();

    public bool leverFlipped = false;
    /*
    bool arrayEqual(int[] a, int[] b)
    {
        if (a.Length != b.Length) return false;

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }*/

    int[] deadEnds = { 2, 4, 101, 105, 107 };
    bool hitDeadEnd = false;

    public IEnumerator turnTrain()
    {
        Debug.Log("FLIP A LEVER");

        trackMover.turn = true;

        while (trackMover.turnAnim.GetCurrentAnimatorStateInfo(0).IsName("Nothing"))
            yield return null;

        while (trackMover.turning) 
            yield return null;

        if (hitDeadEnd)
        {
            Debug.Log("Hit dead end!");
            leverPulls.RemoveAt(leverPulls.Count - 2);
            hitDeadEnd = false;
        }

        bool foundValue = false;
        leverPulls.Add(Convert.ToInt32(leverFlipped));
        for (int i = 0; i < tracksLookup.Keys.Count; i++)
        {
            if (leverPulls.ToArray().SequenceEqual(tracksLookup.ElementAt(i).Key))
            {
                Debug.Log(tracksLookup.ElementAt(i).Value);
                if (deadEnds.Contains(tracksLookup.ElementAt(i).Value)) hitDeadEnd = true;
                foundValue = true;
            }
        }
        if (!foundValue)
        {
            leverPulls.RemoveAt(leverPulls.Count - 1);
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(turnTrain());
        }
    }
}
