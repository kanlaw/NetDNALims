using IMClient.Controls.Base;
using IMClient.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace IMClient.XMPP.Forms
{
    public partial class ImgForm : FrameBase
    {
        Image img = null;
        AnimateImage imgAnimator = null;
        ImageType imgType = ImageType.None;
        int minWidth = 200;
        int minHeight = 200;
        public ImgForm()
        {
            InitializeComponent();
        }

        public ImgForm(string imgPath)
        {
            InitializeComponent();
            //this._imageTag= InitImageTag();
            if (File.Exists(imgPath))
            {

                imgType =Common.CheckImageType(imgPath);
                switch (imgType)
                {
                    case ImageType.GIF:
                        try
                        {
                            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
                            this.img = Image.FromFile(imgPath);
                            imgAnimator = new AnimateImage(this.img);
                            imgAnimator.OnFrameChanged += ImgAnimator_OnFrameChanged;
                            if (this.img.Width >= this.minWidth || this.img.Height >= this.minHeight)
                            {
                                this.Size = img.Size;
                            }
                            else {
                                this.Size = new Size(minWidth, minHeight);
                            }
                        }
                        catch (Exception ex)
                        {

                            
                        }
                        break;
                    case ImageType.None:
                        break;
                    default:
                        try
                        {
                            this.img = Image.FromFile(imgPath);
                            if (this.img.Width >= this.minWidth || this.img.Height >= this.minHeight)
                            {
                                this.Size = img.Size;
                            }
                            else {
                                this.Size = new Size(minWidth, minHeight);
                            }
                        }
                        catch (Exception ex)
                        {
                            this.Close();
                        }
                        break;
                }
           
              
            }
        }

        private void ImgAnimator_OnFrameChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void ImgForm_Shown(object sender, EventArgs e)
        {
            if (this.imgType == ImageType.GIF && this.imgAnimator != null)
            {
                this.imgAnimator.Play();
            }
        }

        private void ImgForm_Paint(object sender, PaintEventArgs e)
        {
            if (this.img != null && imgType!= ImageType.None)
            {
                Image tmpImg = null;
                switch (imgType)
                {
                    case ImageType.None:
                        break;
                    case ImageType.GIF:

                        lock(imgAnimator.Image)
                        {
                            tmpImg = imgAnimator.Image;
                        }
                        break;
                    default:
                        tmpImg = this.img;
                       
                        break;
                }
                Graphics g = e.Graphics;
                //大于最小宽高 拉伸
                if (tmpImg.Width >= this.minWidth || tmpImg.Height >= this.minHeight)
                {
                    g.DrawImage(tmpImg, this.ClientRectangle);
                }
                //小于最小宽高 居中
                else if (tmpImg.Width < this.minWidth && tmpImg.Height < this.minHeight)
                {
                    float x = (minWidth - tmpImg.Width) / 2 < 0 ? 0 : (minWidth - tmpImg.Width) / 2;
                    float y = (minHeight - tmpImg.Height) / 2 < 0 ? 0 : (minHeight - tmpImg.Height) / 2;
                    g.DrawImage(tmpImg, new PointF(x, y));
                }
                else {
                    float ratio = 1.0f;
                    float w_ration = this.Size.Width / tmpImg.Width;
                    float h_ration = this.Size.Height / tmpImg.Height;
                    if (w_ration > 0 && h_ration < 0)
                    {
                        ratio = h_ration;
                    }
                    else if (w_ration > 0 && h_ration < 0)
                    {
                        ratio = w_ration;
                    }
                    else if (w_ration < 0 && h_ration < 0)
                    {
                        ratio = (w_ration < h_ration) ? w_ration : h_ration;
                    }
                    float width = tmpImg.Width * ratio;
                    float height = tmpImg.Height * ratio;
                    float x = (this.Size.Width - width) / 2 < 0 ? 0 : (this.Size.Width - tmpImg.Width) / 2;
                    float y = (this.Size.Height - height) / 2 < 0 ? 0 : (this.Size.Height - tmpImg.Height) / 2;
                    RectangleF rf = new RectangleF(new PointF(x, y), new SizeF(width, height));
                    g.DrawImage(tmpImg, rf);
                }

                
            }
        }

        #region 判断图片类型
        /*
        private  SortedDictionary<int, ImageType> _imageTag;

        public static readonly string ErrType = ImageType.None.ToString();

        private  SortedDictionary<int, ImageType> InitImageTag()
        {
            SortedDictionary<int, ImageType> list = new SortedDictionary<int, ImageType>();

            list.Add((int)ImageType.BMP, ImageType.BMP);
            list.Add((int)ImageType.JPG, ImageType.JPG);
            list.Add((int)ImageType.GIF, ImageType.GIF);
            list.Add((int)ImageType.PCX, ImageType.PCX);
            list.Add((int)ImageType.PNG, ImageType.PNG);
            list.Add((int)ImageType.PSD, ImageType.PSD);
            list.Add((int)ImageType.RAS, ImageType.RAS);
            list.Add((int)ImageType.SGI, ImageType.SGI);
            list.Add((int)ImageType.TIFF, ImageType.TIFF);
            return list;

        }

        /// <summary>  
        /// 通过文件头判断图像文件的类型  
        /// </summary>  
        /// <param name="path"></param>  
        /// <returns></returns>  
        public  string CheckImageTypeName(string path)
        {
            return CheckImageType(path).ToString();
        }
        /// <summary>  
        /// 通过文件头判断图像文件的类型  
        /// </summary>  
        /// <param name="path"></param>  
        /// <returns></returns>  
        public  ImageType CheckImageType(string path)
        {
            byte[] buf = new byte[2];
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int i = sr.BaseStream.Read(buf, 0, buf.Length);
                    if (i != buf.Length)
                    {
                        return ImageType.None;
                    }
                }
            }
            catch (Exception exc)
            {
                //Debug.Print(exc.ToString());
                return ImageType.None;
            }
            return CheckImageType(buf);
        }

        /// <summary>  
        /// 通过文件的前两个自己判断图像类型  
        /// </summary>  
        /// <param name="buf">至少2个字节</param>  
        /// <returns></returns>  
        public  ImageType CheckImageType(byte[] buf)
        {
            if (buf == null || buf.Length < 2)
            {
                return ImageType.None;
            }

            int key = (buf[1] << 8) + buf[0];
            ImageType s;
            try
            {
                if (_imageTag.TryGetValue(key, out s))
                {
                    return s;
                }
            }
            catch (Exception ex)
            {

            }
            return ImageType.None;
        }
    

    /// <summary>  
    /// 图像文件的类型  
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
    */
        #endregion

        #region drawImage
        public void PaintImg()
        {

        }

        public void PaintGif()
        {

        }
        #endregion


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
