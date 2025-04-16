using System.Collections;
using System.Windows.Forms;

namespace DrivingLessonsBooking.UI.Helpers
{
    /// <summary>
    /// Helper class for sorting ListView items based on column values
    /// </summary>
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
