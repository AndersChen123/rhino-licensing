using Rhino.Licensing.Logging;
using System;
using System.IO;

namespace Rhino.Licensing
{
    /// <summary>
    /// License validator validates a license file
    /// that can be located on disk.
    /// </summary>
    public class LicenseValidator : AbstractLicenseValidator
    {
        /// <summary>
        /// License validator logger
        /// </summary>
        private static readonly ILog Log = LogProvider.GetLogger(typeof(LicenseValidator));

        private readonly string licensePath;
        private string inMemoryLicense;

        /// <summary>
        /// Creates a new instance of <seealso cref="LicenseValidator"/>.
        /// </summary>
        /// <param name="publicKey">public key</param>
        /// <param name="licensePath">path to license file</param>
        public LicenseValidator(string publicKey, string licensePath)
            : base(publicKey)
        {
            this.licensePath = licensePath;
        }

        /// <summary>
        /// Gets or Sets the license content
        /// </summary>
        protected override string License
        {
            get
            {
                return inMemoryLicense ?? File.ReadAllText(licensePath);
            }
            set
            {
                try
                {
                    File.WriteAllText(licensePath, value);
                }
                catch (Exception e)
                {
                    inMemoryLicense = value;
                    Log.Warn("[Licensing] Could not write new license value, using in memory model instead", e);
                }
            }
        }

        /// <summary>
        /// Validates loaded license
        /// </summary>
        public override void AssertValidLicense()
        {
            if (File.Exists(licensePath) == false)
            {
                Log.WarnFormat("[Licensing] Could not find license file: {0}", licensePath);
                throw new LicenseFileNotFoundException(string.Format("Could not find license file: {0}", licensePath));
            }

            base.AssertValidLicense();
        }
    }
}
