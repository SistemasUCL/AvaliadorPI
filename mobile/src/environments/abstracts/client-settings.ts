export class IClientSettings {
    public authority: string;
    public client_id: string;
    public redirect_uri: string;
    public post_logout_redirect_uri: string;
    public response_type: string;
    public scope: string;
    public filterProtocolClaims: boolean;
    public loadUserInfo: boolean;
}
