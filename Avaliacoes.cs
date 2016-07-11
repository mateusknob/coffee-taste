using CoffeeTaste.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeTaste
{
    public partial class Avaliacoes : Form
    {
        private string codigo;

        public Avaliacoes()
        {
            InitializeComponent();
            ResetForm();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.OpenForms["Main"].Show();
            Application.OpenForms["Main"].Focus();
        }

        private void ResetForm(string code = "")
        {
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            pnlDados.Visible = false;

            dgvAvaliacoes.Columns.Clear();
            dgvAvaliacoes.Rows.Clear();
            dgvAvaliacoes.RowHeadersVisible = false;

            dgvAvaliacoes.ReadOnly = true;
            dgvAvaliacoes.AllowUserToAddRows = false;
            dgvAvaliacoes.MultiSelect = false;

            dgvAvaliacoes.Columns.Add("Codigo", "Código");
            dgvAvaliacoes.Columns.Add("Data", "Data/Hora");
            dgvAvaliacoes.Columns.Add("Descricao", "Descrição");
            dgvAvaliacoes.Columns.Add("Rotulo", "Rótulo");
            dgvAvaliacoes.Columns.Add("Cor", "Cor");
            dgvAvaliacoes.Columns.Add("Nota", "Nota");

            int selectedIndex = 0;
            foreach (Avaliacao obj in ((Main)Application.OpenForms["Main"]).manager.Avaliacoes.OrderByDescending(x => x.DataHora))
            {
                /*string percepcoes = "";
                foreach (string percepcao in obj.Percepcoes)
                {
                    percepcoes = percepcoes + ", " + percepcao;
                }
                if (percepcoes.Length > 1)
                {
                    percepcoes = percepcoes.Substring(2);
                }*/

                string notaGeral = "";
                for (int i = 0; i < obj.NotaGeral; i++)
                {
                    notaGeral = notaGeral + "★";
                }
                
                /*while(notaGeral.Length < 5)
                {
                    notaGeral = notaGeral + "☆";
                }*/

                int aux = dgvAvaliacoes.Rows.Add(
                    obj.Codigo,
                    obj.DataHora,
                    obj.Descricao,
                    obj.Rotulo.Descricao,
                    Enums.Format(obj.Cor.ToString()),
                    notaGeral
                );
                if (!String.IsNullOrEmpty(code) && obj.Codigo.ToString() == code)
                {
                    selectedIndex = aux;
                }
            }

            foreach (DataGridViewRow row in dgvAvaliacoes.Rows)
            {
                row.Cells[4].Style.BackColor = ColorTranslator.FromHtml(row.Cells[4].Value.ToString());
                row.Cells[4].Style.ForeColor = ColorTranslator.FromHtml(row.Cells[4].Value.ToString());
                row.Cells[4].Style.SelectionBackColor = ColorTranslator.FromHtml(row.Cells[4].Value.ToString());
                row.Cells[4].Style.SelectionForeColor = ColorTranslator.FromHtml(row.Cells[4].Value.ToString());

                row.Cells[5].Style.ForeColor = ColorTranslator.FromHtml("#cccc00");
                row.Cells[5].Style.SelectionForeColor = ColorTranslator.FromHtml("#cccc00");
            }

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font(dgvAvaliacoes.Font, FontStyle.Bold);
            dgvAvaliacoes.Columns["Nota"].DefaultCellStyle = style;

            dgvAvaliacoes.Columns["Codigo"].Width = 50;

            if (dgvAvaliacoes.Rows.Count > 0)
            {
                dgvAvaliacoes.Rows[selectedIndex].Selected = true;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            FormAvaliacao(Convert.ToString(dgvAvaliacoes.SelectedRows[0].Cells[0].Value), false);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string avaliacaoId = Convert.ToString(dgvAvaliacoes.SelectedRows[0].Cells[0].Value);
            string avaliacaoDescricao = Convert.ToString(dgvAvaliacoes.SelectedRows[0].Cells[2].Value);

            DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja remover a Avaliação \"" + avaliacaoId + " - " + avaliacaoDescricao + "\"?", "Confirmação", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ((Main)Application.OpenForms["Main"]).manager.Remove(((Main)Application.OpenForms["Main"]).manager.Avaliacoes.Where(x => x.Codigo.ToString() == avaliacaoId).First());
                ResetForm();
            }
        }

        private void dgvAvaliacoes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAvaliacoes.SelectedRows.Count == 1)
            {
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
                FormAvaliacao(Convert.ToString(dgvAvaliacoes.SelectedRows[0].Cells[0].Value), true);
            }
            else
            {
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            FormAvaliacao();
        }

        private void FormAvaliacao(string codigo = "", bool readOnly = false)
        {
            this.codigo = codigo;
            ResetFormAvaliacao();

            if (!String.IsNullOrEmpty(codigo))
            {
                FillFormAvaliacao();
            }

            foreach (var control in pnlDados.Controls)
            {
                if (control is Button)
                {
                    if (control == btnPercepcao)
                    {
                        ((Button)control).Enabled = !readOnly;
                    }
                    else
                    {
                        ((Button)control).Visible = !readOnly;
                    }
                }
                else if (control is TextBox)
                {
                    ((TextBox)control).ReadOnly = readOnly;
                }
                else if (control is ComboBox)
                {
                    ((ComboBox)control).Enabled = !readOnly;
                }
                else if (control is CheckBox)
                {
                    ((CheckBox)control).Enabled = !readOnly;
                }
                else if (control is ListBox)
                {
                    ((ListBox)control).Enabled = !readOnly;
                }
                else if (control is NumericUpDown)
                {
                    ((NumericUpDown)control).Enabled = !readOnly;
                }
                else if (control is DateTimePicker)
                {
                    ((DateTimePicker)control).Enabled = !readOnly;
                }
                else if (control is Panel)
                {
                    ((Panel)control).Enabled = !readOnly;
                }
                else if (control is RadioButton)
                {
                    ((RadioButton)control).Enabled = !readOnly;
                }
                else if (control is GroupBox)
                {
                    ((GroupBox)control).Enabled = !readOnly;
                }
            }

            pnlDados.Visible = true;
        }

        private void FillFormAvaliacao()
        {
            Avaliacao obj = ((Main)Application.OpenForms["Main"]).manager.Avaliacoes.Where(x => x.Codigo.ToString() == codigo).First();

            dtpDataHora.Value = obj.DataHora;
            txtDescricao.Text = obj.Descricao;

            cmbRotulo.SelectedIndex = cmbRotulo.FindString(obj.Rotulo.Codigo + " - " + obj.Rotulo.Descricao);
            cmbCrema.SelectedIndex = cmbCrema.FindString(Enums.Format(obj.Crema.ToString()));
            cmbHumor.SelectedIndex = cmbHumor.FindString(Enums.Format(obj.Humor.ToString()));
            cmbXicara.SelectedIndex = cmbXicara.FindString(Enums.Format(obj.Xicara.ToString()));

            foreach (Control rad in gbxCor.Controls)
            {
                if (rad is RadioButton)
                {
                    if (rad.Text == obj.Cor.ToString().Replace("H_", "#"))
                    {
                        ((RadioButton)rad).Checked = true;
                    }
                }
            }

            cbxLatte.Checked = obj.Latte;
            cbxDuplo.Checked = obj.Duplo;

            nudTemperatura.Value = obj.Temperatura;
            nudTempoExtracao.Value = obj.SegundosExtração;
            nudTempoMoagem.Value = obj.DiasDesdeMoagem;

            foreach (string value in obj.Percepcoes)
            {
                lstPercepcoes.Items.Add(value);
            }

            txtObservacoes.Text = obj.Observacoes;

            foreach (Control rad in gbxNotaGeral.Controls)
            {
                if (rad is RadioButton)
                {
                    if (rad.Text == obj.NotaGeral.ToString())
                    {
                        ((RadioButton)rad).Checked = true;
                    }
                }
            }

            foreach (Control rad in gbxNivelAfterTaste.Controls)
            {
                if (rad is RadioButton)
                {
                    if (rad.Text == obj.NivelAfterTaste.ToString())
                    {
                        ((RadioButton)rad).Checked = true;
                    }
                }
            }
        }

        private void ResetFormAvaliacao()
        {
            cmbCrema.Items.Clear();
            cmbHumor.Items.Clear();
            cmbRotulo.Items.Clear();
            cmbXicara.Items.Clear();

            dtpDataHora.Format = DateTimePickerFormat.Custom;
            dtpDataHora.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDataHora.Value = DateTime.Now;

            txtDescricao.Text = String.Empty;

            foreach (Rotulo value in ((Main)Application.OpenForms["Main"]).manager.Rotulos)
            {
                string name = value.Codigo + " - " + value.Descricao;
                cmbRotulo.Items.Add(new Item(value.Codigo, name));
            }

            foreach (Enums.Crema value in Enum.GetValues(typeof(Enums.Crema)))
            {
                string name = Enums.Format(Enum.GetName(typeof(Enums.Crema), value));
                cmbCrema.Items.Add(new Item(value.ToString(), name));
            }

            foreach (Enums.Humor value in Enum.GetValues(typeof(Enums.Humor)))
            {
                string name = Enums.Format(Enum.GetName(typeof(Enums.Humor), value));
                cmbHumor.Items.Add(new Item(value.ToString(), name));
            }

            foreach (Enums.Xicara value in Enum.GetValues(typeof(Enums.Xicara)))
            {
                string name = Enums.Format(Enum.GetName(typeof(Enums.Xicara), value));
                cmbXicara.Items.Add(new Item(value.ToString(), name));
            }

            foreach (Control pnl in gbxCor.Controls)
            {
                if (pnl is Panel)
                {
                    ((Panel)pnl).BackColor = ColorTranslator.FromHtml(pnl.Name.Replace("H_", "#"));
                }
            }

            cbxLatte.Checked = false;
            cbxDuplo.Checked = false;

            nudTemperatura.DecimalPlaces = 1;
            nudTemperatura.Maximum = 100;
            nudTemperatura.Minimum = 50;

            nudTempoExtracao.DecimalPlaces = 0;
            nudTempoExtracao.Maximum = 120;
            nudTempoExtracao.Minimum = 0;

            nudTempoMoagem.DecimalPlaces = 0;
            nudTempoMoagem.Maximum = 365;
            nudTempoMoagem.Minimum = 0;

            lstPercepcoes.Items.Clear();

            txtObservacoes.Text = "";

            cmbRotulo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHumor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCrema.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbXicara.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbRotulo.Sorted = true;
            cmbHumor.Sorted = true;
            cmbCrema.Sorted = true;
            cmbXicara.Sorted = true;

            // Default values
            txtDescricao.Text = "Espresso";
            if (cmbRotulo.Items.Count > 0)
            {
                cmbRotulo.SelectedIndex = 0;
            }
            cmbCrema.SelectedIndex = cmbCrema.FindString(Enums.Format(Enums.Crema.Normal.ToString()));
            cmbHumor.SelectedIndex = cmbHumor.FindString(Enums.Format(Enums.Humor.Indiferente.ToString()));
            cmbXicara.SelectedIndex = cmbXicara.FindString(Enums.Format(Enums.Xicara.Dellanno.ToString()));
            foreach (Control rad in gbxCor.Controls)
            {
                if (rad is RadioButton)
                {
                    if (rad.Text == "#694a1b")
                    {
                        ((RadioButton)rad).Checked = true;
                    }
                }
            }
            cbxDuplo.Checked = true;
            nudTemperatura.Value = Convert.ToDecimal(67);
            nudTempoExtracao.Value = Convert.ToDecimal(25);
            nudTempoMoagem.Value = Convert.ToDecimal(10);
            foreach (Control rad in gbxNotaGeral.Controls)
            {
                if (rad is RadioButton)
                {
                    if (rad.Text == "3")
                    {
                        ((RadioButton)rad).Checked = true;
                    }
                }
            }
            foreach (Control rad in gbxNivelAfterTaste.Controls)
            {
                if (rad is RadioButton)
                {
                    if (rad.Text == "3")
                    {
                        ((RadioButton)rad).Checked = true;
                    }
                }
            }
        }

        private string Persist()
        {
            try
            {
                Avaliacao obj, _obj;
                if (String.IsNullOrEmpty(codigo))
                {
                    obj = null;
                    _obj = new Avaliacao();
                }
                else
                {
                    obj = ((Main)Application.OpenForms["Main"]).manager.Avaliacoes.Where(x => x.Codigo.ToString() == codigo).First();

                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new MemoryStream();
                    using (stream)
                    {
                        formatter.Serialize(stream, obj);
                        stream.Seek(0, SeekOrigin.Begin);
                        _obj = (Avaliacao)formatter.Deserialize(stream);
                    }
                }
                _obj.Percepcoes = new List<String>();

                if (((Main)Application.OpenForms["Main"]).manager.Avaliacoes.OrderByDescending(x => x.Codigo).Count() > 0)
                {
                    _obj.Codigo = ((Main)Application.OpenForms["Main"]).manager.Avaliacoes.OrderByDescending(x => x.Codigo).First().Codigo + 1;
                }
                else
                {
                    _obj.Codigo = 1;
                }

                _obj.DataHora = dtpDataHora.Value;
                _obj.Descricao = txtDescricao.Text;

                string codigoRotulo = cmbRotulo.SelectedItem.ToString().Substring(0, cmbRotulo.SelectedItem.ToString().IndexOf(" ")).Trim();
                _obj.Rotulo = ((Main)Application.OpenForms["Main"]).manager.Rotulos.Where(x => x.Codigo == codigoRotulo).First();
                _obj.Crema = (Enums.Crema)Enum.Parse(typeof(Enums.Crema), Enums.Format(cmbCrema.SelectedItem.ToString(), true));
                _obj.Humor = (Enums.Humor)Enum.Parse(typeof(Enums.Humor), Enums.Format(cmbHumor.SelectedItem.ToString(), true));
                _obj.Xicara = (Enums.Xicara)Enum.Parse(typeof(Enums.Xicara), Enums.Format(cmbXicara.SelectedItem.ToString(), true));
                
                _obj.Cor = (Enums.Cor)Enum.Parse(typeof(Enums.Cor), Enums.Format(gbxCor.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked).Text, true));
                _obj.Latte = cbxLatte.Checked;
                _obj.Duplo = cbxDuplo.Checked;

                _obj.Temperatura = nudTemperatura.Value;
                _obj.SegundosExtração = Convert.ToInt32(nudTempoExtracao.Value);
                _obj.DiasDesdeMoagem = Convert.ToInt32(nudTempoMoagem.Value);

                foreach (var item in lstPercepcoes.Items)
                {
                    _obj.Percepcoes.Add(item.ToString());
                }

                _obj.Observacoes = txtObservacoes.Text;
                _obj.NotaGeral = Convert.ToInt32(gbxNotaGeral.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked).Text);
                _obj.NivelAfterTaste = Convert.ToInt32(gbxNivelAfterTaste.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked).Text);

                if (((Main)Application.OpenForms["Main"]).manager.Avaliacoes.Where(x => x.Codigo.ToString() != codigo && x.Codigo.ToString() == _obj.Codigo.ToString()).Count() == 0)
                {
                    if (obj != null)
                    {
                        ((Main)Application.OpenForms["Main"]).manager.Remove(obj);
                    }
                    ((Main)Application.OpenForms["Main"]).manager.Add(_obj);
                }
                else
                {
                    MessageBox.Show("Já existe uma avaliação com este código.");
                    return "";
                }
                return _obj.Codigo.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return "";
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtDescricao.Text.Trim().Length == 0)
            {
                MessageBox.Show("Campos em branco");
            }
            else
            {
                string code = Persist();
                if (!String.IsNullOrEmpty(code))
                {
                    FormAvaliacao();
                    ResetFormAvaliacao();
                    ResetForm(code);
                }
            }
        }

        private void btnPercepcao_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPercepcao.Text))
            {
                lstPercepcoes.Items.Add(txtPercepcao.Text);
                txtPercepcao.Text = "";
            }
        }

        private void lstPercepcoes_DoubleClick(object sender, EventArgs e)
        {
            if (lstPercepcoes.SelectedIndex > -1)
            {
                string item = lstPercepcoes.SelectedItem.ToString();
                lstPercepcoes.Items.Remove(item);
            }
        }
    }
}
