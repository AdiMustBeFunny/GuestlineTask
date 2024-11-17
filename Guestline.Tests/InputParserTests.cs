using Guestline.ConsoleApp.ConsoleParameters;
using Guestline.ConsoleApp.UserInputParser;

namespace Guestline.Tests
{
    public class InputParserTests
    {
        private readonly InputParser _inputParser = new();

        [Theory]
        [InlineData("asdfasfasfasfasasdasdasdasdasdf")]
        [InlineData("            asdfasfasfasfasf")]
        [InlineData("availability(a,12312312,a)")]
        public void Parse_LineDoesntStartWithNameKnownCommand_ShouldThrowArgumentException(string line)
        {
            //arrange
            Action act = () => _inputParser.Parse(line);

            //act and assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData("asdfasfasfasfasf")]
        [InlineData("            asdfasfa")]
        [InlineData("availability(a,12312312,a")]
        public void Parse_LineHasFewerCharsThanMinimalEnforcement_ShouldThrowArgumentException(string line)
        {
            //arrange
            Action act = () => _inputParser.Parse(line);

            //act and assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData("asdfasfasfzxcxzcxasfasf")]
        [InlineData("            q3213213213213ghfdhdfhasdfasfzxczzza")]
        [InlineData("availability(a,12312312,a")]
        public void Parse_LineDoesntEntWithClosingBracket_ShouldThrowArgumentException(string line)
        {
            //arrange
            Action act = () => _inputParser.Parse(line);

            //act and assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData("Availability(a12312312,a)")]
        [InlineData("Availability(a12312312,a,1,a)")]
        public void Parse_CommandHasTooFewOrTooManyParameters_ShouldThrowArgumentException(string line)
        {
            //arrange
            Action act = () => _inputParser.Parse(line);

            //act and assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData("Availability(a,202409011,b)")]
        [InlineData("Availability(a,2024090112,b)")]
        [InlineData("Availability(a,202409011-20240902,b)")]
        [InlineData("Availability(a,202409011-202409023,b)")]
        [InlineData("Availability(a,20240901-202409023a,b)")]
        public void Parse_WrongDateParameter_ShouldThrowArgumentException(string line)
        {
            //arrange
            Action act = () => _inputParser.Parse(line);

            //act and assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData("Availability(a,20240933,b)")]
        [InlineData("Availability(a,20241301,b)")]
        [InlineData("Availability(a,20240001,b)")]
        [InlineData("Availability(a,20240101-20241301,b)")]
        [InlineData("Availability(a,20240101-20240935,b)")]
        public void Parse_DateParameterOutOfRange_ShouldThrowArgumentException(string line)
        {
            //arrange
            Action act = () => _inputParser.Parse(line);

            //act and assert
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData("Availability(a,20240101-20240101,b)")]
        [InlineData("Availability(a,20240102-20240101,b)")]
        public void Parse_DateParameterArrivalIsGreaterOrEquaLToDeparture_ShouldThrowArgumentException(string line)
        {
            //arrange
            Action act = () => _inputParser.Parse(line);

            //act and assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData("Availability(a.json,20240101-20240102,b.json)")]
        [InlineData("Availability(a.json,20240102,b.json)")]
        public void Parse_DateParameterIsCorrect_ShouldReturnInputData(string line)
        {
            //act
            var result = _inputParser.Parse(line);

            //assert
            Assert.NotNull(result);
        }
    }
}
