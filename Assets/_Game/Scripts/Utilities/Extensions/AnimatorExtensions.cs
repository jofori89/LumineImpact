using UnityEngine;

namespace _Game.Scripts.Utilities.Extensions
{
    public static class AnimatorExtensions
    {
        public static bool IsAnimationPlaying(this Animator anim, string stateName)
        {
            return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                   anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
        }
    }
}