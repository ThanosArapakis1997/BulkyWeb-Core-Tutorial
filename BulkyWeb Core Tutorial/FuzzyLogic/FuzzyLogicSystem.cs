namespace MGTConcerts.FuzzyLogic
{
    using FLS;
    using FLS.Rules;

    public class FuzzyLogicSystem
    {
        private FuzzyEngine fuzzyEngine;
        private LinguisticVariable price;
        private LinguisticVariable preference;
        private LinguisticVariable distance;
        private LinguisticVariable recommendation;

        // Constructor to initialize the fuzzy engine and sets
        public FuzzyLogicSystem()
        {
            InitializeFuzzySystem();
        }

        // Method to initialize the fuzzy engine and membership functions
        private void InitializeFuzzySystem()
        {
            // Create the fuzzy engine
            fuzzyEngine = (FuzzyEngine)new FuzzyEngineFactory().Default();

            // Define fuzzy variable for Price (0 to 200)
            price = new LinguisticVariable("Price");
            var priceLow = price.MembershipFunctions.AddTriangle("Low", 0, 0, 100);  // Peak at 0, end at 100
            var priceMedium = price.MembershipFunctions.AddTriangle("Medium", 50, 100, 150);  // Peak at 100
            var priceHigh = price.MembershipFunctions.AddTriangle("High", 100, 200, 200);  // Peak at 200

            // Define fuzzy variable for Preference (0 to 100)
            preference = new LinguisticVariable("Preference");
            var preferenceLow = preference.MembershipFunctions.AddTriangle("Low", 0, 0, 30);      // Peak at 0, end at 30
            var preferenceMedium = preference.MembershipFunctions.AddTriangle("Medium", 20, 50, 80);  // Peak at 50
            var preferenceHigh = preference.MembershipFunctions.AddTriangle("High", 70, 100, 100);    // Peak at 100


            // Define fuzzy variable for Distance (0 to 142 km)
            // 142 is the maximum value based on the random
            // initializations we give to the user location and the music venue location
            distance = new LinguisticVariable("Distance");
            var distanceNear = distance.MembershipFunctions.AddTriangle("Near", 0, 0, 50);  // Peak at 0, end at 50
            var distanceModerate = distance.MembershipFunctions.AddTriangle("Moderate", 40, 71, 100);  // Peak at 71 (half of 142), start and end adjusted
            var distanceFar = distance.MembershipFunctions.AddTriangle("Far", 80, 142, 142);  // Peak at 142, end at 142


            // Define fuzzy output variable for Recommendation (0 to 10)
            recommendation = new LinguisticVariable("Recommendation");
            var recommendationLow = recommendation.MembershipFunctions.AddTriangle("Low", 0, 0, 4);  // Peak at 0, end at 4
            var recommendationMedium = recommendation.MembershipFunctions.AddTriangle("Medium", 3, 5, 7);  // Peak at 5
            var recommendationHigh = recommendation.MembershipFunctions.AddTriangle("High", 6, 10, 10);  // Peak at 10

            // Add rules to the fuzzy engine
            //TODO: FTIAKSE TOUS KANONES OLOUS 
            fuzzyEngine.Rules.Add(
                Rule.If(price.Is(priceLow).And(preference.Is(preferenceHigh)).And(distance.Is(distanceNear))).Then(recommendation.Is(recommendationHigh)),
                Rule.If(price.Is(priceMedium).And(preference.Is(preferenceMedium)).And(distance.Is(distanceModerate))).Then(recommendation.Is(recommendationMedium)),
                Rule.If(price.Is(priceHigh).And(preference.Is(preferenceLow)).And(distance.Is(distanceFar))).Then(recommendation.Is(recommendationLow)),
                Rule.If(price.Is(priceLow).And(preference.Is(preferenceMedium)).And(distance.Is(distanceFar))).Then(recommendation.Is(recommendationMedium)),
                Rule.If(price.Is(priceHigh).And(preference.Is(preferenceHigh)).And(distance.Is(distanceNear))).Then(recommendation.Is(recommendationHigh)),
                Rule.If(price.Is(priceMedium).And(preference.Is(preferenceLow)).And(distance.Is(distanceModerate))).Then(recommendation.Is(recommendationLow))
            );


        }

            // Method to get the recommendation based on input values
        public double GetRecommendation(double inputPrice, double inputPreference, double inputDistance)
        {
            var result = fuzzyEngine.Defuzzify(new
            {
                Price = inputPrice,
                Preference = inputPreference,
                Distance = inputDistance
            });

            return result;
        }
    }

}
