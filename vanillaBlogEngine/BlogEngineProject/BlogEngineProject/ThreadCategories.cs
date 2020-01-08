using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineProject
{
    public static class ThreadCategories
    {
        private static string[] CATEGORIES = new string[3]{ "technology", "education/tutorials", "lifestyle" };
        public static String GetCategory(int categorySelector)
        {
            string finalOutput = " ";
            switch(categorySelector)
            {
                // technology
                case 0:
                    finalOutput = CATEGORIES[0];
                    break;

                // education/tutorials
                case 1:
                    finalOutput = CATEGORIES[1];
                    break;

                // lifestyle
                case 2:
                    finalOutput = CATEGORIES[2];
                    break;
            }

            // returns empty string if there was no number specified
            return finalOutput;
        }

        public static int NumberOfCategories()
        {
            return CATEGORIES.Length;
        }
    }
}
