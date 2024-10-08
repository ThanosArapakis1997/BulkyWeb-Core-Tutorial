namespace MGTConcerts.FuzzyLogic
{
    public class DistanceFuzzySet
    {
        // Membership function for "Close" (0,20,30)
        public double Close(double distance)
        {
            if (distance <= 20)
                return 1;
            if (distance > 20 && distance <= 30)
                return (30 - distance) / 10.0;
            return 0;
        }

        // Membership function for "Medium"(20,50,70)
        public double Medium(double distance)
        {
            if (distance <= 20 || distance >= 70)
                return 0;
            if (distance > 20 && distance <= 50)
                return (distance - 20) / 30.0;
            if (distance > 50 && distance < 70)
                return (70 - distance) / 20.0;
            return 0;
        }

        // Membership function for "Far" (60,80,100)
        public double Far(double distance)
        {
            if (distance <= 60)
                return 0;
            if (distance > 60 && distance <= 80)
                return (distance - 60) / 20.0;
            if (distance > 80)
                return 1;
            return 0;
        }
    }


}
