using System.IO;

namespace Rhino.Licensing.Tests
{
    public class BaseLicenseTest
    {
        public readonly static string public_and_private;
        public readonly static string public_only;

        static BaseLicenseTest()
        {
            public_and_private = new StreamReader(typeof (Can_generate_and_validate_key)
                                                      .Assembly
                                                      .GetManifestResourceStream(
                                                      "Rhino.Licensing.Tests.public_and_private.xml"))
                .ReadToEnd();

            public_only = new StreamReader(typeof (Can_generate_and_validate_key)
                                               .Assembly
                                               .GetManifestResourceStream("Rhino.Licensing.Tests.public_only.xml"))
                .ReadToEnd();


        }

    }
}