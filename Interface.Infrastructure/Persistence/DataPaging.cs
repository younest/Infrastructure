
using System;
using System.Collections.Generic;

using Common.Logging;

namespace Interface.Infrastructure.Persistence
{
    public class DataPaging
    {
        private int _pages;

        private int _records;

        private int _pageSize { get; set; }

        public int Pages
        {
            get
            {
                return _pages;
            }
        }

        public int Records
        {
            get
            {
                return _records;
            }
        }

        private static ILog logeer = LogManager.GetLogger("Persistence");

        public void Initialize(int pageSize, int records)
        {
            //页面大小
            _pageSize = pageSize;
            //总条数
            _records = records;

            //页大小等于0则不分页
            if (_pageSize == 0) { _pages = 1; return; }

            //大于0则按页大小分页
            decimal result = (decimal)Records / pageSize;
            _pages = Convert.ToInt32((Math.Ceiling(result)));
        }

        public List<T> GetCurrentPageEntities<T>(int page, List<T> table)
        {
            if (page < Pages - 1)
            {
                logeer.InfoFormat("起始索引:{0};截至索引:{1}", page * _pageSize, page * _pageSize + _pageSize);
                return table.GetRange(page * _pageSize, _pageSize);
            }
            else
            {
                logeer.InfoFormat("起始索引:{0};截至索引:{1}", page * _pageSize, Records);
                return table.GetRange(page * _pageSize, Records - page * _pageSize);
            }
        }

        public void GetCurrentPageIndex(ref int page, out int startIndex, out int endIndex)
        {
            if (page < Pages - 1)
            {
                startIndex = page * _pageSize + 1;
                endIndex = page * _pageSize + _pageSize;
            }
            else
            {
                startIndex = page * _pageSize;
                endIndex = Records;
            }

            logeer.InfoFormat("起始索引:{0};截至索引:{1}", startIndex, endIndex);
        }
    }
}
