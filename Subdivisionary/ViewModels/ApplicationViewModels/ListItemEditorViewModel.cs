namespace Subdivisionary.ViewModels.ApplicationViewModels
{
    public class ListItemEditorViewModel
    {
        public object Data { get; set; }

        public string Key { get; set; }

        public bool AddRemoveButton { get; set; } = false;

        public string GetPartialViewEditor()
        {
            return "CollectionEditors/_" + Data.GetType().Name + "Editor";
        }
    }
}