using System;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// Represents just 1 signature block
    /// </summary>
    public class SignatureViewModel
    {
        /// <summary>
        /// Name of Signer
        /// </summary>
        public string SignerName { get; set; }

        /// <summary>
        /// Datestamp if signature is signed
        /// </summary>
        public DateTime DateStamp { get; set; }
        /// <summary>
        /// User Loggin Stamp if signatre is signed
        /// </summary>
        public string UserStamp { get; set; }

        /// <summary>
        /// This has to do with JSiganture API. We serialize at
        /// Base32 for its digital footprint and modifiability
        /// </summary>
        public string SerializationType { get; set; }

        /// <summary>
        /// represents the completed signature raw data that can be
        /// used to generate the signature image
        /// </summary>
        public string SignatureData { get; set; }

        /// <summary>
        /// Represents if a singature is completed
        /// </summary>
        public bool IsSignatureFinalized { get; set; }
    }
}