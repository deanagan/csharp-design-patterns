using System.Threading;
using System.Threading.Tasks;
using Adapter;
using NSubstitute;
using Shouldly;
using Xunit;


public class CreditBureauAdapterTests
{
    [Fact]
    public async Task EquifaxCreditAdapter_Should_NormalizeScoreAndMapFields_Correctly()
    {
        // Arrange
        var mockClient = Substitute.For<IEquifaxApiClient>();
        string sampleXml = "<EquifaxResponse><Score>720</Score><Defaults>false</Defaults></EquifaxResponse>";
        
        mockClient.FetchXmlReportAsync("APP-123").Returns(Task.FromResult(sampleXml));

        var adapter = new EquifaxCreditAdapter(mockClient);

        // Act
        var result = await adapter.GetCreditReportAsync("APP-123", CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ApplicantId.ShouldBe("APP-123");
        
        // 720 on an 850 scale normalized to 1000 scale: (int)(720 * (1000.0 / 850.0)) = 847
        result.NormalizedScore.ShouldBe(847);
        result.HasDefaults.ShouldBeFalse();
        result.RiskTier.ShouldBe("Prime"); // 847 > 750
    }

    [Fact]
    public async Task ExperianCreditAdapter_Should_MapDirectly_WhenValuesAreNative()
    {
        // Arrange
        var mockClient = Substitute.For<IExperianApiClient>();
        var sampleDto = new ExperianResponseDto(ScoreValue: 880, BankruptcyFound: true, RiskCategory: "Tier_A");

        mockClient.GetJsonReportAsync("APP-456").Returns(Task.FromResult(sampleDto));

        var adapter = new ExperianCreditAdapter(mockClient);

        // Act
        var result = await adapter.GetCreditReportAsync("APP-456", CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ApplicantId.ShouldBe("APP-456");
        result.NormalizedScore.ShouldBe(880); // Experian uses 1000 scale natively
        result.HasDefaults.ShouldBeTrue();    // BankruptcyFound mapped to HasDefaults
        result.RiskTier.ShouldBe("Prime");    // "Tier_A" mapped to "Prime"
    }
}