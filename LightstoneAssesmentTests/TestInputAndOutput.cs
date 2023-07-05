using LightstoneAssessment;

namespace LightstoneAssesmentTests
{
    public class TestInputAndOutput
    {
        [Fact]
        public void TestInputAndOutput_tests()
        {
            var input = @"";
            ReverseStringInput reversed = new ReverseStringInput();
            string expected = @"";
            input = reversed.InsertInputData();
            reversed.BuildOutputReversed(input);
            var actual = @"";
            Assert.Equal(expected, actual);
        }
    }
}