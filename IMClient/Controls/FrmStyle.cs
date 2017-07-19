using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IMClient.Controls
{
    class FrmStyle
    {
        #region --->通用<---

        /// <summary>
        /// 所有标题的颜色
        /// </summary>
        public static Color fc = Color.FromArgb(105, 105, 105);

        /// <summary>
        /// 提示框大小
        /// </summary>
        public static Size MsgSize = new Size(290, 160);

        /// <summary>
        /// 比对数据综合
        /// </summary>
        public const int compoareAll =  10000;//120 * 10000;

        /// <summary>
        /// 分配基础值
        /// </summary>
        public const int compareBase =  10000 / 5; //24 * 10000;

        //文本颜色
        public static readonly Color TextColor = Color.FromArgb(0xff, 0xff, 0xff);
        //文本字体
        public static readonly Font TextFont = new Font("微软雅黑", 10f);
        //标题字体
        public static readonly Font TitleFont = new Font("微软雅黑", 14f);
        //文本颜色
        public static readonly Color TitleColor = Color.DarkGray;
        //页面字体
        public static readonly Font PageFont = new Font("微软雅黑", 12f);
        //文本框背景色
        public static readonly Color TextBoxBackColor = Color.FromArgb(0xaa, 0xaa, 0xaa);
        //边框颜色
        public static readonly Color BorderColor = Color.FromArgb(169, 169, 169);
        //大标题颜色
        public static readonly Color BigTitleColor = Color.FromArgb(98, 122, 134);
        //大标题字体
        public static readonly Font BigTitleFont = new Font("微软雅黑", 12f,FontStyle.Bold);

        #region CheckBox
        public static readonly Size CheckBoxSize = new Size(20, 18);
        public const int CheckBoxHeight = 24;
        #endregion CheckBox

        #region ColorButton
        //ColorButton默认大小
        public static readonly Size ColorButtonSize = new Size(96, 36);
        //ColorButton默认BackColor
        public static readonly Color ColorButtonColor = Color.FromArgb(0x29, 0xaa, 0xe1);
        //ColorButton默认BorderColor
        public static readonly Color ColorButtonBorderColor = Color.FromArgb(0xff, 0xff, 0xff);
        #endregion ColorButton

        #region IconButton
        public const int IconButtonBorder = 5;
        #endregion IconButton

        #region ScrollBar
        public const int ScrollBarSize = 18;
        public const int ScrollBarDirectionButtonHeadSize = 18;
        public const int ScrollBarDirectionButtonSideSize = 18;
        public const int ScrollBarSliderHeadSize = 18;
        public const int ScrollBarSliderSideMinSize = 4;
        #endregion ScrollBar

        #endregion --->通用<---

        #region --->窗体<---

        #region -->FrmTemple<--
        public static readonly Size FrmTempleMainBTNSize = new Size(200, 50);
        public static readonly Size FrmTempleBTNSynSize = new Size(100, 30);
        public const float FrmTempleMainButtonTopPercent = 0.55f;
        public static readonly Size TemplateUnitSize = new Size(128, 32);
        public const int TemplateUnitGap = 8;
        public static readonly Size TemplateUnitIconSize = new Size(32, 32);
        #endregion -->FrmTemple<--

        #region -->FrmOriginal<--
        public static readonly Size FrmOriginalMainBTNSize = new Size(200, 50);
        public static readonly Size FrmOriginalBTNBakSize = new Size(100, 30);
        public const float FrmOriginalMainButtonTopPercent = 0.55f;
        #endregion -->FrmOriginal<--

        #region -->FrmUpdate<--
        public static readonly Size FrmUpdateMainBTNSize = new Size(200, 50);
        public static readonly Size FrmUpdateBTNHisSize = new Size(100, 30);
        public const float FrmUpdateMainButtonTopPercent = 0.55f;
        public const String FrmUpdateOldVersionText = "您当前的客户端版本是：";
        public const String FrmUpdateNewVersionText = "至最新版本：";
        public const int FrmUpdateHorizontalGap = 128;
        public const int FrmUpdateVerticalGap = 24;
        #endregion -->FrmUpdate<--

        //文本颜色
        public static readonly Color TitleColor2 = Color.FromArgb(54, 114, 144);
        //窗体标题位置
        public static readonly Point FormTitleLocation = new Point(8, 5);
        //标题背景色
        public static readonly Color FormTitleBackColor = Color.FromArgb(44, 155, 205);
        //背景反色
        public static readonly Color FormTitleBackColorReseve = Color.FromArgb(211, 100, 50);
        //标题背景色
        public static readonly Color FormTitleFontColor = Color.FromArgb(255, 255, 255);
        //标题高度
        public const int FormTitleHeight = 32;
        //Form背景色
        public static readonly Color FormBackColor = Color.FromArgb(255, 255, 255);
        public static readonly Color FormOverColor = Color.FromArgb(0x4d, 0x4d, 0x4d);
        public static readonly Color FormDownColor = Color.FromArgb(0x33, 0x33, 0x33);
        //Form组件间距
        public const int FormGap = 16;

        #region 消息框
        //按钮文本
        public const string MessageBoxTextOK = "确定";
        public const string MessageBoxTextCancel = "取消";
        public const string MessageBoxTextYes = "是";
        public const string MessageBoxTextNo = "否";
        //消息框的图标尺寸
        public static readonly Size MessageBoxIconSize = new Size(48, 48);
        //消息框组件之间的间隔
        public const int MessageBoxGap = 32;
        #endregion 消息框

        #region 案件上报
        //item
        public static readonly Size CaseUploadItemIconSize = new Size(36, 36);
        public const int CaseUploadItemHeight = 48;
        public const int CaseUploadItemGap = 8;
        public const int CaseUploadItemFieldWidth = 64;
        public static readonly Size CaseUploadItemCheckBoxSize = new Size(20, 18);
        //list
        public const int CaseUploadListTitleHeight = 48;
        public const int CaseUploadListToolBarHeight = 36;
        public const int CaseUploadListGap = 8;
        public static readonly Color CaseUploadListBackColor = Color.FromArgb(0x4d, 0x4d, 0x4d);
        public static readonly Size CaseUploadListFigureSize = new Size(28, 28);
        public static readonly Size CaseUploadListButtonSize = new Size(64, 24);
        public static readonly Color SearchTextBoxBackColor = Color.FromArgb(0xaa, 0xaa, 0xaa);
        public static readonly Color SearchButtonBackColor = Color.FromArgb(0x66, 0x66, 0x66);
        public const int SearchButtonWidth = 36;
        public const int SearchTextWidth = 160;
        #endregion 案件上报

        #region 案件过滤
        public static readonly Color CaseFilterItemBackColor = Color.FromArgb(0x4d, 0x4d, 0x4d);
        public static readonly Color CaseFilterTextOverColor = Color.FromArgb(0xff, 0xff, 0x00);
        public static readonly Color CaseFilterTextDownColor = Color.FromArgb(0xff, 0x7f, 0x7f);
        public const int CaseFilterItemHeight = 32;
        public const int CaseFilterItemGap = 8;
        public static readonly Color CaseFilterColor = Color.FromArgb(0x33, 0x33, 0x33);
        public const int CaseFilterHeight = 32;
        public const int CaseFilterGap = 8;
        public static readonly Size CaseFilterFoldingIconSize = new Size(8, 8);
        #endregion 案件过滤

        #region 设置选项
        //选项卡
        public static readonly Color OptionMenuItemBackColor = Color.FromArgb(0x66, 0x66, 0x66);
        public static readonly Color OptionMenuItemActiveColor = Color.FromArgb(0x4d, 0x4d, 0x4d);
        public const int OptionMenuItemSplitThickness = 1;
        public static readonly Color OptionMenuItemSplitColor = Color.FromArgb(0xff, 0xff, 0xff);
        public const int OptionMenuItemGap = 8;
        public const int OptionMenuItemLabelWidth = 128;
        public const int OptionMenuTitleHeight = 48;
        public const int OptionMenuStateBarHeight = 36;
        public static readonly Color OptionMenuBackColor = Color.FromArgb(0x33, 0x33, 0x33);
        //选项列表
        public const int OptionListTitleHeight = 32;
        public static readonly Color OptionListBackColor = Color.FromArgb(0x33, 0x33, 0x33);
        public static readonly Color OptionListItemBackColor = Color.FromArgb(0x4d, 0x4d, 0x4d);
        public static readonly Color OptionListItemDownColor = Color.FromArgb(0x33, 0x33, 0x33);
        public const int OptionListItemHeight = 32;
        public const int OptionListItemGap = 8;
        public static readonly Color OptionListItemTextOverColor = Color.FromArgb(0xff, 0xff, 0x00);
        public static readonly Color OptionListItemTextDownColor = Color.FromArgb(0xff, 0x7f, 0x7f);
        #endregion 设置选项

        #endregion --->窗体<---
    }
}
