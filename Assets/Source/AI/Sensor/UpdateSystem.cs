namespace Sensor
{
    public class UpdateSystem
    {
        public void Update()
        { 
            for (int i = 0; i < GameState.SensorManager.length; i++)
            {
                ref Sensor sensor = ref GameState.SensorManager.Get(i);

                if (sensor.Tick <= sensor.TickSinceLast)
                {
                    sensor.TickSinceLast = 0;
                    switch (sensor.Type)
                    {
                        case SensorType.Sight:
                            Sight.Update(sensor);
                            break;
                        case SensorType.Hearing:
                            break;
                    }
                }
                else 
                {
                    // Todo: Do proper tick count.
                    sensor.TickSinceLast++;
                }
            }
        }
    }
}
