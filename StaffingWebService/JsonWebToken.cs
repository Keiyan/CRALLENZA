//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;
//using System.Security.Cryptography;
//using System.Text;

//namespace StaffingWebService
//{
//    // Reference: http://tools.ietf.org/search/draft-jones-json-web-token-00
//    //
//    // JWT is made up of 3 parts: Envelope, Claims, Signature.
//    // - Envelope - specifies the token type and signature algorithm used to produce 
//    //       signature segment. This is in JSON format
//    // - Claims - specifies claims made by the token. This is in JSON format
//    // - Signature - Cryptographic signature use to maintain data integrity.
//    // 
//    // To produce a JWT token:
//    // 1. Create Envelope segment in JSON format
//    // 2. Create Claims segment in JSON format
//    // 3. Create signature
//    // 4. Base64url encode each part and append together separated by "." 
    
//    public class JsonWebToken
//    {
//        #region Helper Classes

//        [DataContract]
//        public class JsonWebTokenClaims
//        {
//            private DateTime? expiration;

//            [DataMember(Name = "exp")]
//            private int expUnixTime { get; set; }

//            public DateTime Expiration
//            {
//                get
//                {
//                    if (expiration == null)
//                    {
//                        expiration = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(expUnixTime);
//                    }

//                    return (DateTime) expiration;
//                }
//            }

//            [DataMember(Name = "iss")]
//            public string Issuer { get; private set; }

//            [DataMember(Name = "aud")]
//            public string Audience { get; private set; }

//            [DataMember(Name = "uid")]
//            public string UserId { get; private set; }

//            [DataMember(Name = "ver")]
//            public int Version { get; private set; }

//            [DataMember(Name = "urn:microsoft:appuri")]
//            public string ClientIdentifier { get; private set; }

//            [DataMember(Name = "urn:microsoft:appid")]
//            public string AppId { get; private set; }
//        }

//        [DataContract]
//        public class JsonWebTokenEnvelope
//        {
//            [DataMember(Name = "typ")]
//            public string Type { get; private set; }

//            [DataMember(Name = "alg")]
//            public string Algorithm { get; private set; }

//            [DataMember(Name = "kid")]
//            public int KeyId { get; private set; }
//        }

//        #endregion

//        #region Properties

//        private static readonly DataContractJsonSerializer ClaimsJsonSerializer =
//            new DataContractJsonSerializer(typeof (JsonWebTokenClaims));

//        private static readonly DataContractJsonSerializer EnvelopeJsonSerializer =
//            new DataContractJsonSerializer(typeof (JsonWebTokenEnvelope));

//        private static readonly UTF8Encoding UTF8Encoder = new UTF8Encoding(true, true);
//        private static readonly SHA256Managed SHA256Provider = new SHA256Managed();

//        private readonly string claimsTokenSegment;

//        private readonly string envelopeTokenSegment;
//        public JsonWebTokenClaims Claims { get; private set; }
//        public JsonWebTokenEnvelope Envelope { get; private set; }

//        public string Signature { get; private set; }

//        public bool IsExpired
//        {
//            get { return Claims.Expiration < DateTime.Now; }
//        }

//        #endregion

//        #region Constructors

//        public JsonWebToken(string token, Dictionary<int, string> keyIdsKeys)
//        {
//            // Get the token segments & perform validation
//            string[] tokenSegments = SplitToken(token);

//            // Decode and deserialize the claims
//            claimsTokenSegment = tokenSegments[1];
//            Claims = GetClaimsFromTokenSegment(claimsTokenSegment);

//            // Decode and deserialize the envelope
//            envelopeTokenSegment = tokenSegments[0];
//            Envelope = GetEnvelopeFromTokenSegment(envelopeTokenSegment);

//            // Get the signature
//            Signature = tokenSegments[2];

//            // Ensure that the tokens KeyId exists in the secret keys list
//            if (!keyIdsKeys.ContainsKey(Envelope.KeyId))
//            {
//                throw new Exception(string.Format("Could not find key with id {0}", Envelope.KeyId));
//            }

//            // Validation
//            ValidateEnvelope(Envelope);
//            ValidateSignature(keyIdsKeys[Envelope.KeyId]);
//        }

//        private JsonWebToken()
//        {
//        }

//        #endregion

//        #region Parsing Methods

//        private JsonWebTokenClaims GetClaimsFromTokenSegment(string claimsTokenSegment)
//        {
//            byte[] claimsData = Base64UrlDecode(claimsTokenSegment);
//            using (var memoryStream = new MemoryStream(claimsData))
//            {
//                return ClaimsJsonSerializer.ReadObject(memoryStream) as JsonWebTokenClaims;
//            }
//        }

//        private JsonWebTokenEnvelope GetEnvelopeFromTokenSegment(string envelopeTokenSegment)
//        {
//            byte[] envelopeData = Base64UrlDecode(envelopeTokenSegment);
//            using (var memoryStream = new MemoryStream(envelopeData))
//            {
//                return EnvelopeJsonSerializer.ReadObject(memoryStream) as JsonWebTokenEnvelope;
//            }
//        }

//        private string[] SplitToken(string token)
//        {
//            // Expected token format: Envelope.Claims.Signature

//            if (string.IsNullOrEmpty(token))
//            {
//                throw new Exception("Token is empty or null.");
//            }

//            string[] segments = token.Split('.');

//            if (segments.Length != 3)
//            {
//                throw new Exception("Invalid token format. Expected Envelope.Claims.Signature");
//            }

//            if (string.IsNullOrEmpty(segments[0]))
//            {
//                throw new Exception("Invalid token format. Envelope must not be empty");
//            }

//            if (string.IsNullOrEmpty(segments[1]))
//            {
//                throw new Exception("Invalid token format. Claims must not be empty");
//            }

//            if (string.IsNullOrEmpty(segments[2]))
//            {
//                throw new Exception("Invalid token format. Signature must not be empty");
//            }

//            return segments;
//        }

//        #endregion

//        #region Validation Methods

//        private void ValidateEnvelope(JsonWebTokenEnvelope envelope)
//        {
//            if (envelope.Type != "JWT")
//            {
//                throw new Exception("Unsupported token type");
//            }

//            if (envelope.Algorithm != "HS256")
//            {
//                throw new Exception("Unsupported crypto algorithm");
//            }
//        }

//        private void ValidateSignature(string key)
//        {
//            // Derive signing key, Signing key = SHA256(secret + "JWTSig")
//            byte[] bytes = UTF8Encoder.GetBytes(key + "JWTSig");
//            byte[] signingKey = SHA256Provider.ComputeHash(bytes);

//            // To Validate:
//            // 
//            // 1. Take the bytes of the UTF-8 representation of the JWT Claim
//            //  Segment and calculate an HMAC SHA-256 MAC on them using the
//            //  shared key.
//            //
//            // 2. Base64url encode the previously generated HMAC as defined in this
//            //  document.
//            //
//            // 3. If the JWT Crypto Segment and the previously calculated value
//            //  exactly match in a character by character, case sensitive
//            //  comparison, then one has confirmation that the key was used to
//            //  generate the HMAC on the JWT and that the contents of the JWT
//            //  Claim Segment have not be tampered with.
//            //
//            // 4. If the validation fails, the token MUST be rejected.

//            // UFT-8 representation of the JWT envelope.claim segment
//            byte[] input = UTF8Encoder.GetBytes(envelopeTokenSegment + "." + claimsTokenSegment);

//            // calculate an HMAC SHA-256 MAC
//            using (var hashProvider = new HMACSHA256(signingKey))
//            {
//                byte[] myHashValue = hashProvider.ComputeHash(input);

//                // Base64 url encode the hash
//                string base64urlEncodedHash = Base64UrlEncode(myHashValue);

//                // Now compare the two has values
//                if (base64urlEncodedHash != Signature)
//                {
//                    throw new Exception("Signature does not match.");
//                }
//            }
//        }

//        #endregion

//        #region Base64 Encode / Decode Functions

//        // Reference: http://tools.ietf.org/search/draft-jones-json-web-token-00

//        public byte[] Base64UrlDecode(string encodedSegment)
//        {
//            string s = encodedSegment;
//            s = s.Replace('-', '+'); // 62nd char of encoding
//            s = s.Replace('_', '/'); // 63rd char of encoding
//            switch (s.Length%4) // Pad with trailing '='s
//            {
//                case 0:
//                    break; // No pad chars in this case
//                case 2:
//                    s += "==";
//                    break; // Two pad chars
//                case 3:
//                    s += "=";
//                    break; // One pad char
//                default:
//                    throw new Exception("Illegal base64url string");
//            }
//            return Convert.FromBase64String(s); // Standard base64 decoder
//        }

//        public string Base64UrlEncode(byte[] arg)
//        {
//            string s = Convert.ToBase64String(arg); // Standard base64 encoder
//            s = s.Split('=')[0]; // Remove any trailing '='s
//            s = s.Replace('+', '-'); // 62nd char of encoding
//            s = s.Replace('/', '_'); // 63rd char of encoding
//            return s;
//        }

//        #endregion
//    }

//    [JsonObject(MemberSerialization.OptIn)]
//    public class User
//    {
//        [JsonProperty(PropertyName = "id")]
//        public string Id { get; set; }

//        [JsonProperty(PropertyName = "name")]
//        public string Name { get; set; }

//        [JsonProperty(PropertyName = "first_name")]
//        public string FirstName { get; set; }

//        [JsonProperty(PropertyName = "last_name")]
//        public string LastName { get; set; }

//        [JsonProperty(PropertyName = "gender")]
//        public string Gender { get; set; }

//        [JsonProperty(PropertyName = "locale")]
//        public string Locale { get; set; }

//        [JsonProperty(PropertyName = "emails")]
//        public Emails emails { get; set; }
//    }

//    [JsonObject(MemberSerialization.OptIn)]
//    public class Emails
//    {
//        [JsonProperty(PropertyName = "preferred")]
//        public string Preferred { get; set; }

//        [JsonProperty(PropertyName = "account")]
//        public string Account { get; set; }

//        [JsonProperty(PropertyName = "personnal")]
//        public string Personnal { get; set; }

//        [JsonProperty(PropertyName = "business")]
//        public string Business { get; set; }
//    }


//    struct TokenInfo
//    {
//        public User user;
//        public DateTime ExpirationDate;

//        public TokenInfo( User user)
//        {
//            this.user = user;
//            this.ExpirationDate = DateTime.Now.AddMinutes(15);
//        }
//    }
//}