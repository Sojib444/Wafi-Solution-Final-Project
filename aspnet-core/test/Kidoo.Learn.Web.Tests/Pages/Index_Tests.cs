using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Kidoo.Learn.Pages;

public class Index_Tests : LearnWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
