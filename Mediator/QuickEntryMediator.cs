using System;
using System.Windows.Forms;

namespace PatternsUncleBob.Mediator
{
    public class QuickEntryMediator
    {
        private readonly TextBox itsTextBox;
        private readonly ListBox itsList;

        public QuickEntryMediator(TextBox itsTextBox, ListBox itsList)
        {
            this.itsTextBox = itsTextBox;
            this.itsList = itsList;
            itsTextBox.TextChanged += new EventHandler(TextFieldChanged);
        }

        private void TextFieldChanged(object source, EventArgs args)
        {
            string prefix = itsTextBox.Text;
            if (prefix.Length == 0)
            {
                itsList.ClearSelected();
                return;
            }
            ListBox.ObjectCollection listItems = itsList.Items;
            bool found = false;
            for (int i = 0; !found && i < listItems.Count; i++)
            {
                object o = listItems[i];
                string s = o.ToString();
                if (s.StartsWith(prefix))
                {
                    itsList.SetSelected(i, true);
                    found = true;
                }
            }
            if (!found)
            {
                itsList.ClearSelected();
            }
        }
    }
}