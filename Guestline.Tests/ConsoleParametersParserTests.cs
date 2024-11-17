using Guestline.ConsoleApp.ConsoleParameters;

namespace Guestline.Tests
{
    public class ConsoleParametersParserTests
    {
        private readonly ConsoleParametersParser _consoleParametersParser = new();

        [Theory]
        [InlineData()]
        [InlineData("argument1")]
        [InlineData("argument1", "argument2")]
        [InlineData("argument1", "argument2", "argument3")]
        [InlineData("argument1", "argument2", "argument3", "argument4", "argument5")]
        public void Parse_InvalidArgumentCount_ShouldThrowArgumentException(params string[] args)
        {
            //arrange
            Action act = () => _consoleParametersParser.Parse(args);

            //act and assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData("argument1", "argument2", "argument3", "argument4")]
        [InlineData("--hotels", "argument2", "argument3", "argument4")]
        [InlineData("argument1", "argument2", "--hotels", "argument4")]
        [InlineData("argument1", "argument2", "--bookings", "argument4")]
        [InlineData("--bookings", "argument2", "argument3", "argument4")]
        [InlineData("--bookings", "--bookings", "argument3", "argument4")]
        [InlineData("--bookings", "argument2", "argument3", "--hotels")]
        [InlineData("--bookings", "argument2", "--bookings", "argument4")]
        [InlineData("--hotels", "argument2", "--hotels", "argument4")]
        public void Parse_ValidArgumentCountButArgumentsAreIncorrect_ShouldThrowArgumentException(params string[] args)
        {
            //arrange
            Action act = () => _consoleParametersParser.Parse(args);

            //act and assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.NotNull(exception);
        }

        [Fact]
        public void Parse_HotelArgumentsFirst_ShouldReturnParsedData()
        {
            //arrange
            var hotelFilePath = "hotels.json";
            var args = new string[] { "--hotels", hotelFilePath, "--bookings", "bookings.json" };

            //act
            var parsedData = _consoleParametersParser.Parse(args);

            //assert
            Assert.Equal(parsedData.HotelsFilePath, hotelFilePath);
        }

        [Fact]
        public void Parse_HotelArgumentsSecond_ShouldReturnParsedData()
        {
            //arrange
            var hotelFilePath = "hotels.json";
            var args = new string[] { "--bookings", "bookings.json", "--hotels", hotelFilePath };

            //act
            var parsedData = _consoleParametersParser.Parse(args);

            //assert
            Assert.Equal(parsedData.HotelsFilePath, hotelFilePath);
        }
    }
}