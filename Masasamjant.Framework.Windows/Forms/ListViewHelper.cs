namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Provides helper methods for the <see cref="ListView"/> control.
    /// </summary>
    public static class ListViewHelper
    {
        /// <summary>
        /// Draw custom list view header with default line alignment.
        /// </summary>
        /// <param name="listView">The list view.</param>
        /// <param name="args">The <see cref="DrawListViewColumnHeaderEventArgs"/> instance containing the event data.</param>
        /// <param name="backgroundBrush">The background brush.</param>
        /// <param name="textBrush">The text brush.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="listView"/>, <paramref name="args"/>, <paramref name="backgroundBrush"/>, or <paramref name="textBrush"/> is <c>null</c>.</exception>
        public static void DrawCustomHeader(this ListView listView, DrawListViewColumnHeaderEventArgs args, Brush backgroundBrush, Brush textBrush)
            => DrawCustomHeader(listView, args, backgroundBrush, StringAlignment.Center, textBrush);

        /// <summary>
        /// Draw custom list view header.
        /// </summary>
        /// <param name="listView">The list view.</param>
        /// <param name="args">The <see cref="DrawListViewColumnHeaderEventArgs"/> instance containing the event data.</param>
        /// <param name="backgroundBrush">The background brush.</param>
        /// <param name="lineAlignment">The line alignment.</param>
        /// <param name="textBrush">The text brush.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="listView"/>, <paramref name="args"/>, <paramref name="backgroundBrush"/>, or <paramref name="textBrush"/> is <c>null</c>.</exception>
        public static void DrawCustomHeader(this ListView listView, DrawListViewColumnHeaderEventArgs args, Brush backgroundBrush, StringAlignment lineAlignment, Brush textBrush)
        {
            ArgumentNullException.ThrowIfNull(listView);
            ArgumentNullException.ThrowIfNull(args);
            ArgumentNullException.ThrowIfNull(backgroundBrush);
            ArgumentNullException.ThrowIfNull(textBrush);

            var columnHeader = listView.Columns[args.ColumnIndex];
            args.Graphics.FillRectangle(backgroundBrush, args.Bounds);
            var sf = new StringFormat() {  LineAlignment = lineAlignment };
            args.Graphics.DrawString(columnHeader.Text, args.Font ?? listView.Font, textBrush, args.Bounds, sf);
        }
    }
}
