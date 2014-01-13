using Newtonsoft.Json;
using ProxyApi.MetadataGenerator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyApi.MetadataGenerator
{
    public partial class frmApiProxyConfig : Form
    {
        Configuration configFile;
        public frmApiProxyConfig(Configuration config)
        {
            InitializeComponent();

            this.configFile = config;



            txtName.Text = configFile.Name;
            txtEndpoint.Text = configFile.Endpoint;

        }



        private void btnOk_Click(object sender, EventArgs e)
        {
            var config = new Configuration
            {
                Name = txtName.Text,
                Endpoint = txtEndpoint.Text
            };

            var json = JsonConvert.SerializeObject(config, new JsonSerializerSettings { Formatting = Formatting.Indented });

            File.WriteAllText(Configuration.FileName, json);

            this.Close();
        }
    }
}
