using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAuthTest
{
    public class TokenConfiguration
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; } = "/token";

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; } = "somebigverybigkey";

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        /// <value>
        /// The issuer.
        /// </value>
        public string Issuer { get; set; } = "SignalRAuthTest";

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        /// <value>
        /// The audience.
        /// </value>
        public string Audience { get; set; } = "SignalRAuthTest";

        /// <summary>
        /// Gets or sets the expiration.
        /// </summary>
        /// <value>
        /// The expiration.
        /// </value>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromHours(1);

        /// <summary>
        /// Gets or sets the refresh expiration.
        /// </summary>
        /// <value>
        /// The refresh expiration.
        /// </value>
        public TimeSpan RefreshExpiration { get; set; } = TimeSpan.FromHours(5);
    }
}
