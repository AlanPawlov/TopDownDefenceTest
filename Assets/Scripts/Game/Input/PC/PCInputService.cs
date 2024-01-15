using UnityEngine;

namespace Game.Input.PC
{
    public class PCInputService : InputService
    {
        public override Vector2 Movement
        {
            get
            {
                //TODO: потом на ноывый инпут перенести
                var xAxis = UnityEngine.Input.GetAxis("Horizontal");
                var yAxis = UnityEngine.Input.GetAxis("Vertical");
                return new Vector2(xAxis, yAxis);
            }
        }

        public override void Init()
        { }
    }
}