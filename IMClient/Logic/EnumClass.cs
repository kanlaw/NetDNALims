using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMClient.Logic
{
    /// <summary>
    /// 聊天联系人列表
    /// </summary>
    public enum IMType
    {
        recent=0,friends=1,search=2

    }

    //public class EnumClass
    //{

        /// <summary>
        /// 文件传送时 状态
        /// </summary>
    public enum FileStatus {
        error=-1,send=1,receive=2,cancel=3,complete=4
    }

    public enum UserListType
    {
        findAUser=0,findEmcee=1,findMember=2
    }

    /// <summary>
    /// 文件发送 类型
    /// OnLine 在线
    /// OffLine 离线
    /// </summary>
    public enum SendFileType {
        OnLine,OffLine
    }

    /// <summary>
    /// 文件上传/下载 操作类型
    /// </summary>
    public enum OperationType {
        Download=0,UpLoad=1
    }
        /// <summary>
        /// 图片类型
        /// </summary>
    public enum ImageType
        {
            None = 0,
            BMP = 0x4D42,
            JPG = 0xD8FF,
            GIF = 0x4947,
            PCX = 0x050A,
            PNG = 0x5089,
            PSD = 0x4238,
            RAS = 0xA659,
            SGI = 0xDA01,
            TIFF = 0x4949
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        public enum MessageType
        {
           System=0, Normal=1, Vibration=2, Voice=3,File=4,
        }

        /// <summary>
        /// 系统自动提示消息类型
        /// </summary>
        public enum SysInfoType
        {
            OK=0, Attention=1,Fail=2
        }

     

        /// <summary>
        /// 真实的文件类型
        /// </summary>
        public enum FileType {
            jpg=255216,gif=7173,bmp=6677,png=-1,
            office03 = 208207,office07=8075,
            xls=-98,xlsx=-1,xlsb=-1,xlsm=-1,
            doc=-2,docx=-1,dot=-1,docm=-1,
            ppt=-3,pptx=-1,pot=-1,
            txt=5150, txt2 = 4946, txt3 = 104116,
            rar = 8297,
            exe =7790,
            pdf =3780,
            unKnow=-99
                
        }

    /// <summary>
    /// 图片位置
    /// </summary>
    public enum ImageAlign {
        left=0,center=1,right=2
    }

    public enum SpecialContentType {
        font=0,image=1
    }

    /// <summary>
    /// 会议室状态
    /// </summary>
    public enum MeetingStatus {
        ready=0,    //准备中
        opening =1, //已建立
        closed =2   //关闭
    }

        /*文件扩展名说明
          * 255216 jpg
          * 208207 doc xls ppt wps
          * 8075 docx pptx xlsx zip
          * 5150 txt
          * 8297 rar
          * 7790 exe
          * 3780 pdf      
          * 
          * 4946/104116 txt
          * 7173        gif 
          * 255216      jpg
          * 13780       png
          * 6677        bmp
          * 239187      txt,aspx,asp,sql
          * 208207      xls.doc.ppt
          * 6063        xml
          * 6033        htm,html
          * 4742        js
          * 8075        xlsx,zip,pptx,mmap,zip
          * 8297        rar   
          * 01          accdb,mdb
          * 7790        exe,dll
          * 5666        psd 
          * 255254      rdp 
          * 10056       bt种子 
          * 64101       bat 
          * 4059        sgf    
          */
    }
//}
