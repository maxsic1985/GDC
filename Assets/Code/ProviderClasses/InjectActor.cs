using UnityEngine;

namespace MSuhininTestovoe.B2B
{
    public class InjectActor : MonoBehaviour
    {
        private void Awake()
        {
            IActor actor = GetComponent<IActor>();

            IHaveActor[] needActor = GetComponentsInChildren<IHaveActor>(true);

            foreach (IHaveActor na in needActor)
            {
                na.Actor = actor;
            }
        }
    }
}