using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace IMClient.Controls.ResourceManager
{
    public partial class ResourceManager
    {
        static ResourceManager m_current;

        Dictionary<string, object> m_resources;

        static ResourceManager()
        {
            ResourceManager.m_current = new ResourceManager();
        }

        ResourceManager()
        {
            this.m_resources = new Dictionary<string, object>();
        }

        public static ResourceManager Current
        {
            get { return ResourceManager.m_current; }
        }

        public Image CreateImage(string id, string path)
        {
            string file = Path.Combine(Application.StartupPath, "Resources", path);

            //TODO:Error
            if (!File.Exists(file)) throw new Exception();

            Image img = Image.FromFile(file);
            this.Set<Image>(id, img);

            return img;
        }

        public SolidBrush CreateSolidBrush(string id, int r, int g, int b)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(r, g, b));

            this.Set<Brush>(id, brush);

            return brush;
        }

        public TextureBrush CreateImageBrush(string id, string path)
        {
            string file = Path.Combine(Application.StartupPath, "Resources", path);

            //TODO:Error
            if (!File.Exists(file)) throw new Exception();

            Image img = Image.FromFile(file);
            this.Set<Image>(id, img);

            TextureBrush brush = new TextureBrush(img);

            this.Set<Brush>(id, brush);

            return brush;
        }

        public void Set<TResource>(string id, TResource resource) where TResource : class
        {
            string rid = id.ToLower() + "!" + typeof(TResource).FullName;

            if (this.m_resources.ContainsKey(rid)) this.m_resources[rid] = resource;
            else this.m_resources.Add(rid, resource);
        }

        public TResource Get<TResource>(string id) where TResource : class
        {
            string rid = id.ToLower() + "!" + typeof(TResource).FullName;

            //TODO:Error
            if (!this.m_resources.ContainsKey(rid)) throw new Exception();

            TResource res = this.m_resources[rid] as TResource;

            //TODO:Error
            if (res == null) throw new Exception();

            return res;
        }
    }
}
