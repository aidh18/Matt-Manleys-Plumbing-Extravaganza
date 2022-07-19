using System;


namespace Matt_Manleys_Plumbing_Extravaganza.Game.Casting
{

    /// <summary>
    /// A validator for string and numeric types.
    /// </summary>
    /// <remarks>
    /// The responsibility of Validator is to check the correctness of a value.
    /// </remarks>
    public class Validator
    {


        public static void CheckInRange(int value, int min, int max) 
        {
            if (value < min || value > max) 
            {
                throw new ArgumentOutOfRangeException(
                    $"Value of {value} must be between {min} and {max}.");
            }
        }


        public static void CheckNotBlank(string value) 
        {
            if (value == null || value == string.Empty || value.Replace(" ", "")  == string.Empty)
            {
                throw new ArgumentException("Value can't be blank.");
            }
        }

        public static void CheckNotNull(object reference) 
        {
            if (reference == null) 
            {
                throw new ArgumentNullException("Value can't be null.");
            }
        }
        
    }
}