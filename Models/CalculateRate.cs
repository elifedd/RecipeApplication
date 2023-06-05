using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace RecipeApp.Models
{
    public class CalculateRate
    {
        public static double GetRating(List<int> rates)
        {
            if(rates == null || rates.Count == 0)
            {
                return 0;
            }

            int star5 = rates.Count(x => x == 5);
            int star4 = rates.Count(x => x == 4);
            int star3 = rates.Count(x => x == 3);
            int star2 = rates.Count(x => x == 2);
            int star1 = rates.Count(x => x == 1);

            double rating = (double)(star5 * 5 + star4 * 4 + star3 * 3 + star2 * 2 + star1 * 1) / (star5 + star4 + star3 + star2 + star1);

            return rating;
        }
    }
}
