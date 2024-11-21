
namespace Zeil.CreditCardValidation.Api.Services.Interfaces;

public class LuhnValidationService : ILuhnValidationService
{

    //<summary>Followed from Stripe's guide - as wikipedia's one isnt very clear and stripe deals with a lot a CC
    //https://stripe.com/nz/resources/more/how-to-use-the-luhn-algorithm-a-guide-in-applications-for-businesses#luhn-algorithm-formula
    //In a real project i would use a prebuilt one, thats likely much faster than this eg https://github.com/marcosgiurni/LuhnNet/ </summary>
    public async Task<bool> IsValid(string? number)
    {
        bool isValid = false;
        if (!string.IsNullOrEmpty(number) && number.All(character => int.TryParse(character.ToString(), out _)))
        {
            var cardDigits = number.Select(character => long.Parse(character.ToString()));
            cardDigits = cardDigits.Reverse();//Start from the right going left

            long doublingSum = 0;
            long sumFromNonDoubledDigits = 0;
            for (var index = 0; index < cardDigits.Count(); ++index)
            {
                if (index % 2 == 0) //Every second - double, and maybe add together
                {
                    sumFromNonDoubledDigits += cardDigits.ElementAt(index);
                }
                else
                {
                    var doubledDigit = cardDigits.ElementAt(index) * 2;
                    if (doubledDigit > 9)
                    {
                        doubledDigit = doubledDigit.ToString().AsEnumerable()
                                                            .Select(digit => long.Parse(digit.ToString()))
                                                            .Sum();
                    }
                    doublingSum += doubledDigit;
                }
            }
            var totalSum = doublingSum + sumFromNonDoubledDigits;
            isValid = totalSum % 10 == 0; //If the total sum ends in 0/multiple of 10 then the number is valid.
        }
        return isValid;
    }
}