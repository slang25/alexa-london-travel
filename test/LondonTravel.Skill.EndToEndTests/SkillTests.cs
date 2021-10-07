// Copyright (c) Martin Costello, 2017. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.

using System.Net;
using System.Text.Json;
using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Runtime;
using Environment = System.Environment;

namespace MartinCostello.LondonTravel.Skill;

public class SkillTests
{
    public SkillTests(ITestOutputHelper outputHelper)
    {
        OutputHelper = outputHelper;
    }

    public static IEnumerable<object[]> Payloads
    {
        get
        {
            return Directory.GetFiles("Payloads")
                .Select((p) => Path.GetFileNameWithoutExtension(p))
                .OrderBy((p) => p)
                .Select((p) => new object[] { p })
                .ToArray();
        }
    }

    private ITestOutputHelper OutputHelper { get; }

    [SkippableTheory]
    [MemberData(nameof(Payloads))]
    public async Task Can_Invoke_Intent_Can_Get_Json_Response(string payloadName)
    {
        string accessToken = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
        string secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY");

        Skip.If(
            string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(secretKey),
            "No AWS credentials are configured.");

        string functionName = Environment.GetEnvironmentVariable("LAMBDA_FUNCTION_NAME");

        Skip.If(
            string.IsNullOrEmpty(functionName),
            "No Lambda function name is configured.");

        string regionName = Environment.GetEnvironmentVariable("AWS_REGION");

        Skip.If(
            string.IsNullOrEmpty(functionName),
            "No AWS region name is configured.");

        // Arrange
        string payload = await File.ReadAllTextAsync(Path.Combine("Payloads", $"{payloadName}.json"));

        var credentials = new BasicAWSCredentials(accessToken, secretKey);
        var region = RegionEndpoint.GetBySystemName(regionName);

        using var client = new AmazonLambdaClient(credentials, region);

        var request = new InvokeRequest()
        {
            FunctionName = functionName,
            InvocationType = InvocationType.RequestResponse,
            LogType = LogType.None,
            Payload = payload,
        };

        OutputHelper.WriteLine($"FunctionName: {request.FunctionName}");
        OutputHelper.WriteLine($"Payload: {request.Payload}");

        // Act
        InvokeResponse response = await client.InvokeAsync(request);

        using var reader = new StreamReader(response.Payload);
        string responsePayload = await reader.ReadToEndAsync();

        OutputHelper.WriteLine($"ExecutedVersion: {response.ExecutedVersion}");
        OutputHelper.WriteLine($"FunctionError: {response.FunctionError}");
        OutputHelper.WriteLine($"HttpStatusCode: {response.HttpStatusCode}");
        OutputHelper.WriteLine($"RequestId: {response.ResponseMetadata.RequestId}");
        OutputHelper.WriteLine($"StatusCode: {response.StatusCode}");

        // Assert
        response.HttpStatusCode.ShouldBe(HttpStatusCode.OK);
        response.StatusCode.ShouldBe(200);
        response.FunctionError.ShouldBeNull();
        response.ExecutedVersion.ShouldBe("$LATEST");

        using var document = JsonDocument.Parse(responsePayload);
        document.RootElement.ValueKind.ShouldBe(JsonValueKind.Object);
    }
}
