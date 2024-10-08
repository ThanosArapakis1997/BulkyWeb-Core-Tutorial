namespace MGTConcerts.FuzzyLogic
{
    public class PreferenceFuzzySet
    {
        // Membership function for "Very Low" (0,10,20)
        public double VeryLow(double score)
        {
            if (score <= 10)
                return 1;
            if (score > 10 && score <= 20)
                return (20 - score) / 10.0;
            return 0;
        }
         
        // Membership function for "Low" (10,20,40)
        public double Low(double score)
        {
            if (score <= 10)
                return 0;
            if (score > 10 && score <= 20)
                return (score - 10) / 10.0;
            if (score > 20 && score <= 40)
                return (40 - score) / 20.0;
            return 0;
        }

        // Membership function for "Medium"(30,50,70)
        public double Medium(double score)
        {
            if (score <= 30 || score >= 70)
                return 0;
            if (score > 30 && score <= 50)
                return (score - 30) / 20.0;
            if (score > 50 && score <= 70)
                return (70 - score) / 20.0;
            return 0;
        }

        // Membership function for "High" (60,80,90)
        public double High(double score)
        {
            if (score <= 60)
                return 0;
            if (score > 60 && score <= 80)
                return (score - 60) / 20.0;
            if (score > 80 && score <= 90)
                return (90 - score) / 10.0;
            return 0;
        }

        // Membership function for "Very High" (80,90,100)
        public double VeryHigh(double score)
        {
            if (score <= 80)
                return 0;
            if (score > 80 && score <= 90)
                return (score - 80) / 10.0;
            if (score > 90)
                return 1;
            return 0;
        }
    }

}
