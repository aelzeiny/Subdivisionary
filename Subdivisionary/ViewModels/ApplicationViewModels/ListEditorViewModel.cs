using System.Collections;
using Subdivisionary.Models.Collections;

namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    /// <summary>
    /// ViewModel for ICollection List Editor. Made as an abstract class
    /// for model binding reasons. See ICustomModelBinder to find out why.
    /// </summary>
    public abstract class ListEditorViewModel
    {
        /// <summary>
        /// should the item have a remove (trash) icon?
        /// </summary>
        public bool AddRemoveButton { get; set; } = false;
        /// <summary>
        /// What is the data default for when the new button is clicked and a new
        /// item is initialized?
        /// </summary>
        public object EmptyDataDefault { get; set; }

        /// <summary>
        /// Key that identifies the Collection
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Count for the number of list items
        /// </summary>
        /// <returns>List.Count</returns>
        public abstract int Count();
        /// <summary>
        /// Gets the Item Collection Data
        /// </summary>
        /// <returns>IEnumerable of existing Item Collection</returns>
        public abstract IEnumerable GetList();
    }

    /// <summary>
    /// Direct and Only Implementation of teh ListEditorViewModel Class
    /// </summary>
    /// <typeparam name="T">Collection Item Type</typeparam>
    public class ListEditorViewModel<T> : ListEditorViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Identifing Key</param>
        /// <param name="mList">Collection List</param>
        /// <param name="empty">Empty data Default for when a new item is initialized</param>
        /// <param name="addRemoveButton">Should the remove (trash) icon be added to the item?</param>
        public ListEditorViewModel(string key, SerializableList<T> mList, T empty, bool addRemoveButton = false)
        {
            this.Key = key;
            this.List = mList;
            this.EmptyDataDefault = empty;
            this.AddRemoveButton = addRemoveButton;
        }
        /// <summary>
        /// collection item list
        /// </summary>
        public SerializableList<T> List { get; set; }

        /// <summary>
        /// Gets the Collection List
        /// </summary>
        /// <returns>Collection List</returns>
        public override IEnumerable GetList()
        {
            return List;
        }
        /// <summary>
        /// Gets the Count of the List
        /// </summary>
        /// <returns>List.Count</returns>
        public override int Count()
        {
            return List.Count;
        }
    }
}