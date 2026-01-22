# authie

All fake APIs and Auth mehcanisms to test complex http client calls.

P1 API just uses standard token auth.

Seccl API uses a standard token request with the addition of a seccl-id header to get the token.
	AND each resource request must have x-api-key query param.

Intelliflo API uses a standard auth with the addition of a zzz query param.
	AND each resource request must have sys request header.
