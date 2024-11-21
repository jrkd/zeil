using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Zeil.CreditCardValidation.Api.Services.Interfaces;


namespace Zeil.CreditCardValidation.Api.Tests;

[TestFixture]
public class CreditCardValidationControllerTests
{
    private CreditCardValidationController? creditCardValidationController;
    private Mock<ICreditCardValidationService>? mockedValidationService;
    [SetUp]
    public void Setup()
    {
        mockedValidationService = new Mock<ICreditCardValidationService>();
        creditCardValidationController = new(mockedValidationService.Object);
    }

    [TestCase("INVALID", false)]
    [TestCase(null, false)]
    [TestCase("1", false)]
    [TestCase("1111111111111111111111111111111111111111111", false)] //too many digits
    [TestCase("abcd-bbbb-ffff-gggg", false)] //test incorrect character set
    [TestCase("4111 1111 1111 1111", false)] //Test valid VISA number, but invalid format
    [TestCase("4111-1111-1111-1111", false)] //Test valid VISA number, but invalid format
    public async Task Test_Invalid_Cards(string? cardNumber, bool expectedResult)
    {
        Assert.NotNull(mockedValidationService);
        Assert.NotNull(creditCardValidationController);
        mockedValidationService!.Setup(service => service.IsValid(cardNumber)).ReturnsAsync(expectedResult);

        var actionResult = await creditCardValidationController!.Index();
        Assert.IsInstanceOf<NotFound>(actionResult);
    }


    //List from https://www.windcave.com/support-merchant-frequently-asked-questions-testing-details 
    [TestCase("4111111111111111", true)] //Visa
    [TestCase("4242424242424242", true)] //Visa
    [TestCase("4999999999999103", true)] //Visa
    [TestCase("4999999999999236", true)] //Visa
    [TestCase("4999999999999269", true)] //Visa
    [TestCase("4999999999999996", true)] //Visa
    [TestCase("4999999999999202", true)] //Visa
    [TestCase("5431111111111111", true)]//MC
    [TestCase("5123455806308521", true)]//MC
    [TestCase("5123459046058920", true)]//MC
    [TestCase("542766006424133", true)]//MC
    [TestCase("222300001000000", true)]//MC
    [TestCase("543111111111130", true)]//MC
    [TestCase("5431111111111228", true)]//MC
    [TestCase("5999999999999108", true)]//MC
    [TestCase("377400111111115", true)] //AMEX
    [TestCase("376000000000006", true)] //AMEX
    [TestCase("371111111111114", true)] //AMEX
    [TestCase("371234806987034", true)] //AMEX
    [TestCase("6011111111111117", true)] //Discover
    [TestCase("6601111111111113", true)] //Discover
    [TestCase("6011309900001248", true)] //Discover
    [TestCase("3562350000000003", true)]//JCB
    [TestCase("5000511111111113", true)] //ASB true rewards
    [TestCase("5328650000000006", true)]//Jetstar test cards
    [TestCase("5210000010001001", true)]//Jetstar test cards
    [TestCase("4903111111111113", true)] //Switch
    [TestCase("4026111111111115", true)]//Visa electron
    [TestCase("4065930109000002", true)]//Warehose money visa card
    public async Task Test_Valid_Cards(string? cardNumber, bool expectedResult)
    {
        Assert.NotNull(mockedValidationService);
        Assert.NotNull(creditCardValidationController);
        mockedValidationService!.Setup(service => service.IsValid(cardNumber)).ReturnsAsync(expectedResult);

        var actionResult = await creditCardValidationController!.Index();
        Assert.IsInstanceOf<OkResult>(actionResult);
    }
}