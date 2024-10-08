namespace MGTConcerts.FuzzyLogic
{
    public class PriceFuzzySet
    {
        // Membership function for "Low"
        public double Low(double price)
        {
            if (price <= 15)
                return 1;
            if (price > 15 && price <= 25)
                return (25 - price) / 10.0;
            return 0;
        }

        // Membership function for "Medium-Low"
        public double MediumLow(double price)
        {
            if (price <= 15)
                return 0;
            if (price > 15 && price <= 30)
                return (price - 15) / 15.0;
            if (price > 30 && price <= 50)
                return (50 - price) / 20.0;
            return 0;
        }

        // Membership function for "Medium-High"
        public double MediumHigh(double price)
        {
            if (price <= 40 || price >= 75)
                return 0;
            if (price > 40 && price <= 60)
                return (price - 40) / 20.0;
            if (price > 60 && price <= 75)
                return (75 - price) / 15.0;
            return 0;
        }

        // Membership function for "High"
        public double High(double price)
        {
            if (price <= 65)
                return 0;
            if (price > 65 && price <= 85)
                return (price - 65) / 20.0;
            if (price > 85)
                return 1;
            return 0;
        }
    }

}
