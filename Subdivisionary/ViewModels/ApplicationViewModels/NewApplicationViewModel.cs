using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// New Application View Model for creating new applications
    /// </summary>
    public class NewApplicationViewModel
    {
        /// <summary>
        /// Project Information that will be used to create application
        /// </summary>
        public BasicProjectInfo ProjectInfo { get; set; }

        /// <summary>
        /// Application Type from Enum.
        /// </summary>
        public EApplicationTypeViewModel ApplicationType { get; set; }

        /// <summary>
        /// ASP.NET Model Binding uses system.reflection to create an object.
        /// Thus it needs a constructor without any parameters
        /// </summary>
        public NewApplicationViewModel()
        {
        }

        /// <summary>
        /// View Model Constructor
        /// </summary>
        /// <param name="appTypeId">Application Type Enum (as an int type)</param>
        /// <param name="projInfo">Project Information</param>
        public NewApplicationViewModel(int appTypeId, BasicProjectInfo projInfo) : this((EApplicationTypeViewModel)appTypeId, projInfo)
        {
        }
        /// <summary>
        /// View Model Constructory
        /// </summary>
        /// <param name="appTypeId">Application Type Enum</param>
        /// <param name="projInfo">Project Information</param>
        public NewApplicationViewModel(EApplicationTypeViewModel appType, BasicProjectInfo projInfo)
        {
            this.ApplicationType = appType;
            ProjectInfo = projInfo;
        }
    }
}