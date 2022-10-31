//imports UnityEngine

using KMath;
using System.Collections;
namespace Events
{
    public class DropShipEvent : UnityEngine.MonoBehaviour
    {
        private VehicleEntity vehicle;
        private bool Init;

        private void Start()
        {
            StartCoroutine(SpawnDropShip());
        }

        IEnumerator SpawnDropShip()
        {
            yield return new UnityEngine.WaitForSeconds(1.0f);
            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

            float x = worldPosition.x;
            float y = worldPosition.y;

            vehicle = GameState.Planet.AddVehicle(Enums.VehicleType.DropShip, new Vec2f(x, y));

            Init = true;
            yield return null;
        }

        private void Update()
        {
            if(Init)
            {
                transform.position = new UnityEngine.Vector3(vehicle.vehiclePhysicsState2D.Position.X, 
                    vehicle.vehiclePhysicsState2D.Position.Y, -3);
            }
        }
    }
}
