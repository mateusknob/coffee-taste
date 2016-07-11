using CoffeeTaste.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeTaste
{
    public partial class Main : Form
    {
        public CoffeeTasteManager manager;

        public Main()
        {
            manager = new CoffeeTasteManager();
            manager = manager.GetData();

            InitializeComponent();

            if (manager.Avaliacoes.Count == 0 && manager.Rotulos.Count == 0)
            {
                MessageBox.Show("Nova instância de Coffee Taste iniciada!");
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void btnAvaliacoes_Click(object sender, EventArgs e)
        {
            if (manager.Rotulos.Count() == 0)
            {
                MessageBox.Show("Cadastre pelo menos um rótulo para começar a avaliar");
            }
            else
            {
                Avaliacoes form = new Avaliacoes();
                form.Show();
                this.Hide();
            }
        }

        private void btnRotulos_Click(object sender, EventArgs e)
        {
            Rotulos form = new Rotulos();
            form.Show();
            this.Hide();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string directory = AppDomain.CurrentDomain.BaseDirectory + "\\Backups";
                
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string destFileName = directory + "\\backup_" + DateTime.Now.ToString().Replace(" ", "_").Replace("/", "_").Replace(":", "_").Replace("-", "_") + ".ser";
                File.Copy(manager.fileName, destFileName);

                Process.Start(directory);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao realizar backup: " + ex.Message);
            }
        }
    }
}
