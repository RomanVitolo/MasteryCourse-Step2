using UnityEngine;
using System;

public class AnimationImpactWatcher : MonoBehaviour
{
    public event Action OnImpact;
    
    //Called by Animation
    private void Impact()
    {
        if (OnImpact != null)
        {
            OnImpact();
        }
    }
}
