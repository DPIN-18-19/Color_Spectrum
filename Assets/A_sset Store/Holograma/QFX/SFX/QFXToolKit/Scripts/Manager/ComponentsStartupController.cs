// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class ComponentsStartupController : ControlledObject
    {
        public ControlledObject[] ControlledObjects;

        public override void Run()
        {
            base.Run();
            
            foreach (var controlledObject in ControlledObjects)
            {
                controlledObject.Setup();
                controlledObject.Run();
            }
        }
    }
}