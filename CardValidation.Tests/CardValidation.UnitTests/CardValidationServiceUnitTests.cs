using CardValidation.Core.Enums;
using CardValidation.Core.Services;
using Xunit;

namespace CardValidation.UnitTests
{
    public class CardValidationServiceUnitTests
    {
        private readonly CardValidationService _service = new();

        // -------- OWNER --------

        [Theory]
        [InlineData("John Doe")]
        [InlineData("Jane")]
        [InlineData("John Michael Doe")]
        public void ValidateOwner_ValidNames_ReturnsTrue(string owner)
        {
            var result = _service.ValidateOwner(owner);

            Assert.True(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("John123")]
        [InlineData("John Doe Smith Jr")]
        public void ValidateOwner_InvalidNames_ReturnsFalse(string owner)
        {
            var result = _service.ValidateOwner(owner);

            Assert.False(result);
        }

        // -------- ISSUE DATE --------

        [Theory]
        [InlineData("12/30")]
        [InlineData("01/2028")]
        public void ValidateIssueDate_FutureDates_ReturnsTrue(string issueDate)
        {
            var result = _service.ValidateIssueDate(issueDate);

            Assert.True(result);
        }

        [Theory]
        [InlineData("01/20")]
        [InlineData("13/25")]
        [InlineData("00/24")]
        public void ValidateIssueDate_InvalidDates_ReturnsFalse(string issueDate)
        {
            var result = _service.ValidateIssueDate(issueDate);

            Assert.False(result);
        }

        // -------- CVC --------

        [Theory]
        [InlineData("123")]
        [InlineData("1234")]
        public void ValidateCvc_ValidValues_ReturnsTrue(string cvc)
        {
            var result = _service.ValidateCvc(cvc);

            Assert.True(result);
        }

        [Theory]
        [InlineData("12")]
        [InlineData("abcd")]
        [InlineData("12345")]
        public void ValidateCvc_InvalidValues_ReturnsFalse(string cvc)
        {
            var result = _service.ValidateCvc(cvc);

            Assert.False(result);
        }

        // -------- CARD NUMBER --------

        [Theory]
        [InlineData("4111111111111111")] // Visa
        [InlineData("5555555555554444")] // MasterCard
        [InlineData("378282246310005")]  // AmEx
        public void ValidateNumber_ValidCardNumbers_ReturnsTrue(string cardNumber)
        {
            var result = _service.ValidateNumber(cardNumber);

            Assert.True(result);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("9999999999999999")]
        public void ValidateNumber_InvalidCardNumbers_ReturnsFalse(string cardNumber)
        {
            var result = _service.ValidateNumber(cardNumber);

            Assert.False(result);
        }

        // -------- PAYMENT SYSTEM TYPE --------

        [Fact]
        public void GetPaymentSystemType_Visa_ReturnsVisa()
        {
            var type = _service.GetPaymentSystemType("4111111111111111");

            Assert.Equal(PaymentSystemType.Visa, type);
        }

        [Fact]
        public void GetPaymentSystemType_MasterCard_ReturnsMasterCard()
        {
            var type = _service.GetPaymentSystemType("5555555555554444");

            Assert.Equal(PaymentSystemType.MasterCard, type);
        }

        [Fact]
        public void GetPaymentSystemType_AmericanExpress_ReturnsAmericanExpress()
        {
            var type = _service.GetPaymentSystemType("378282246310005");

            Assert.Equal(PaymentSystemType.AmericanExpress, type);
        }
    }
}