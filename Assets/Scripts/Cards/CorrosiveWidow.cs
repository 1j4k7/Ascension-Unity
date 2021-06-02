using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrosiveWidow : Monster
{
    public override void RewardOnDefeat()
    {
        // Gain 3 honor. Each opponent must put a Construct he controls into his discard pile.
    }
}
