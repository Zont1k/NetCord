﻿namespace NetCord.Rest;

public class Connection : IJsonModel<JsonModels.JsonConnection>
{
    JsonModels.JsonConnection IJsonModel<JsonModels.JsonConnection>.JsonModel => _jsonModel;
    private readonly JsonModels.JsonConnection _jsonModel;

    public Connection(JsonModels.JsonConnection jsonModel, RestClient client)
    {
        _jsonModel = jsonModel;
        if (jsonModel.Integrations != null)
            Integrations = jsonModel.Integrations.ToDictionary(i => i.Id, i => new Integration(i, client));
    }

    /// <summary>
    /// The username of the connection account.
    /// </summary>
    public string Name => _jsonModel.Name;

    /// <summary>
    /// The service of this connection.
    /// </summary>
    public ConnectionType Type => _jsonModel.Type;

    /// <summary>
    /// Whether the connection is revoked.
    /// </summary>
    public bool? Revoked => _jsonModel.Revoked;

    /// <summary>
    /// An array of server integrations.
    /// </summary>
    public IReadOnlyDictionary<ulong, Integration>? Integrations { get; }

    /// <summary>
    /// Whether the connection is verified.
    /// </summary>
    public bool Verified => _jsonModel.Verified;

    /// <summary>
    /// Whether friend sync is enabled for this connection.
    /// </summary>
    public bool FriendSync => _jsonModel.FriendSync;

    /// <summary>
    /// Whether activities related to this connection will be shown in presence updates.
    /// </summary>
    public bool ShowActivity => _jsonModel.ShowActivity;

    /// <summary>
    /// Whether this connection has a corresponding third party OAuth2 token.
    /// </summary>
    public bool TwoWayLink => _jsonModel.TwoWayLink;

    /// <summary>
    /// Visibility of this connection.
    /// </summary>
    public ConnectionVisibility Visibility => _jsonModel.Visibility;
}
