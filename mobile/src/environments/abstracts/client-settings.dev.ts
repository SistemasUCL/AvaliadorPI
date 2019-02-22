export const ClientSettings = {
  authority: 'https://localhost:44301',
  client_id: 'pwa',
  redirect_uri: `${document.location.origin}/callback`,
  post_logout_redirect_uri: `${document.location.origin}/home`,
  response_type: 'id_token token',
  scope: 'openid profile avaliadorpi',
  filterProtocolClaims: true,
  loadUserInfo: true
};
