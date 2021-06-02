using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarOfTheFallen : Monster
{
    public override void RewardOnDefeat()
    {
        // Unbanishable (make sure this property is somewhere). Gain 4 honor. You may acquire or defeat any card in the center row without paying its cost.
    }
}
