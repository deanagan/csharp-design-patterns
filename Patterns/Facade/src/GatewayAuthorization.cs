namespace Facade;

public record GatewayAuthorization(
    bool IsAuthorized,
    string? AuthorizationCode,
    string? GatewayMessage);

