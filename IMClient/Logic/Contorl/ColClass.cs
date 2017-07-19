using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMClient.Logic.Contorl
{
    [Serializable]
    public class ColClass
    {

        public ColClass()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="colName">显示列名</param>
        /// <param name="tabColName">相关数据表列名</param>
        /// <param name="colWidth">宽度 百分比</param>
        public ColClass(string colName, string tabColName, int colWidth)
        {
            this.colName = colName;
            this.colName_Table = tabColName;
            this.colWidth = colWidth;
        }

        /// <summary>
        /// 列名
        /// </summary>
        private string colName;


        /// <summary>
        /// 相关绑定的DT列名
        /// </summary>
        private string colName_Table;



        /// <summary>
        /// 列宽
        /// </summary>
        private int colWidth;

        /// <summary>
        /// 显示的列名
        /// </summary>
        public string ColName { get => colName; set => colName = value; }

        /// <summary>
        /// 绑定的DataTable列
        /// </summary>
        public string ColName_Table { get => colName_Table; set => colName_Table = value; }

        /// <summary>
        /// 列宽 百分比
        /// </summary>
        public int ColWidth { get => colWidth; set => colWidth = value; }
    }
}
