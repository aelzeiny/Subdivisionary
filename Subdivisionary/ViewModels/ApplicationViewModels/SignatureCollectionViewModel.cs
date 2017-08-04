using System.Collections.Generic;
using System.Linq;
using Subdivisionary.Models;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// Helper Collection Class that generates SignatureViewModels for given form
    /// </summary>
    public class SignatureCollectionViewModel
    {
        /// <summary>
        /// ID of form that has signature blocks
        /// </summary>
        public int FormId { get; set; }
        /// <summary>
        /// List of Signatures
        /// </summary>
        public SignatureList Properties { get; set; }
        /// <summary>
        /// List of Signature Upload Information
        /// </summary>
        public ICollection<SignatureUploadInfo> Infos { get; set; }

        /// <summary>
        /// New Signature collection View Model
        /// </summary>
        /// <param name="formId">Form ID</param>
        /// <param name="props">Properties w/i form</param>
        /// <param name="infos">Completed signature uploads + information</param>
        public SignatureCollectionViewModel(int formId, SignatureList props, ICollection<SignatureUploadInfo> infos)
        {
            this.FormId = formId;
            this.Properties = props;
            this.Infos = infos;
        }
        /// <summary>
        /// Generates a list of all SignatureViewModels
        /// </summary>
        /// <returns></returns>
        public SignatureViewModel[] GenerateSignatureViewModels()
        {
            SignatureViewModel[] answers = new SignatureViewModel[Properties.Count];
            for (int i = 0; i < answers.Length; i++)
            {
                var prop = Properties[i];
                answers[i] = new SignatureViewModel()
                {
                    SignerName = prop.SignerName
                };
                if (Infos == null)
                    continue;
                var sig = Infos.FirstOrDefault(x => x.SignerName == prop.SignerName);
                if (sig != null)
                {
                    answers[i].SerializationType = sig.DataFormat;
                    answers[i].SignatureData  = sig.Data;
                    answers[i].DateStamp = sig.DateStamp;
                    answers[i].UserStamp = sig.UserStamp;
                }
            }
            return answers;
        }
    }
}