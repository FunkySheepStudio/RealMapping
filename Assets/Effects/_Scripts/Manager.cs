using UnityEngine;
using UnityEngine.VFX;

namespace FunkySheep.Effects
{
    public class Manager : MonoBehaviour
    {
        VisualEffect effect;
        private void Awake()
        {
            effect = GetComponent<VisualEffect>();
        }
    }
}
