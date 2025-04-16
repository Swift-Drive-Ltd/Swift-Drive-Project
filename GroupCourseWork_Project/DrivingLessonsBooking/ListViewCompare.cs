// This file should be deleted as its functionality has been moved to UI/Helpers/ListViewItemComparer.cs
// Please delete this file manually to avoid ambiguity errors

using System.Collections;
using System.Windows.Forms;

namespace DrivingLessonsBooking
{
    // Helper class for sorting ListView items
    public class ListViewItemComparer : IComparer
    {
        private int col;
        
        public ListViewItemComparer(int column)
        {
            col = column;
        }
        
        public int Compare(object? x, object? y)
        {
            if (x == null || y == null)
                return 0;
                
            return string.Compare(
                ((ListViewItem)x).SubItems[col].Text,
                ((ListViewItem)y).SubItems[col].Text
            );
        }
    }
}
