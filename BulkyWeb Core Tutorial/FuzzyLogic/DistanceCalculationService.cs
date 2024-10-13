namespace MGTConcerts.FuzzyLogic
{
    public class DistanceCalculationService
    {
        public double CalculateDistance(int  userLongtitude, int userLatitude, int venueLongtitude, int venueLatitude)
        {
            //return the euclidean distance between 2 points in  a 2D space

            return Math.Sqrt(Math.Pow(venueLongtitude - userLongtitude, 2) + Math.Pow(venueLatitude - userLatitude, 2));
        }

    }
}
