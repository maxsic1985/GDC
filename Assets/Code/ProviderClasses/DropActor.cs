using UnityEngine;

namespace MSuhininTestovoe.B2B
{
    public class DropActor : Actor
    {
        [SerializeField] private DropType dropType;
        public DropType DropType => dropType;
        public override void Handle()
        {
             
        }
        
    }
}