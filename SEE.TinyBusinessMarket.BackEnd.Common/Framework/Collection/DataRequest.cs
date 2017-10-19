using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEE.Framework.Core.Collection
{
    public enum SortDirection
    {
        Ascending = 1,
        Descending = 2
    }

    public class PagerItem
    {
        public int Index { get; set; }
        public string Caption { get; set; }
        public bool Selected { get; set; }
    }

    public class SortItem
    {
        public string ColumnName { get; set; }
        public string Description { get; set; }

        public SortItem()
        {

        }
        public SortItem(string columnName, string description)
        {
            ColumnName = columnName;
            Description = description;
        }
    }

    public class DataRequest
    {
        #region paging

        /// <summary>
        /// Total number of records found
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Number of records on single page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Current page number (zero based)
        /// </summary>
        public int PageIndex { get; set; }

        // Number of pages
        public int NumOfPages
        {
            get
            {
                if (PageSize > 0)
                {
                    return (int)Math.Ceiling((decimal)TotalRecords / PageSize);
                }
                return 1;
            }
        }

        /// <summary>
        /// Current offset
        /// </summary>
        public int Offset
        {
            get
            {
                if (PageSize > 0)
                {
                    return PageIndex * PageSize;
                }
                return 0;
            }
        }

        /// <summary>
        /// Number of records on current page
        /// </summary>
        public int RecordsOnPage
        {
            get
            {
                if (PageSize == 0)
                {
                    return 0;
                }
                if (TotalRecords < PageSize)
                {
                    return TotalRecords;
                }
                return TotalRecords - Offset > PageSize ? PageSize : TotalRecords - Offset;
            }
        }

        /// <summary>
        /// Determine if paging is enabled
        /// </summary>
        public bool IsPaged
        {
            get
            {
                return PageSize > 0 && TotalRecords > PageSize;
            }
        }

        /// <summary>
        /// Determine if list is not empty
        /// </summary>
        public bool HasRecords
        {
            get
            {
                return TotalRecords > 0;
            }
        }

        /// <summary>
        /// Current direction of sort (1 = Ascending, 2 = Descending)
        /// </summary>
        public SortDirection SortDirection { get; set; }

        /// <summary>
        /// Current sorted by column name
        /// </summary>
        public string SortColumn { get; set; }

        public List<SortItem> SortItems { get; set; }

        public List<PagerItem> PagerItems
        {
            get
            {
                var list = new List<PagerItem>();
                for (int i = 0; i < NumOfPages; i++)
                {
                    list.Add(new PagerItem
                    {
                        Index = i,
                        Caption = (i + 1).ToString(),
                        Selected = i == PageIndex,
                    });
                }
                return list;
            }
        }
        #endregion

        public DataRequest()
        {
            PageSize = 10;
            SortDirection = SortDirection.Ascending;
        }

        public DataRequest(int pageSize, SortDirection sortDirection)
        {
            PageSize = pageSize;
            SortDirection = sortDirection;
        }

        public string Phrase { get; set; }
    }
}
