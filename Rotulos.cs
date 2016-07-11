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
    public partial class Rotulos : Form
    {
        private string codigo;

        public Rotulos()
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

            dgvRotulos.Columns.Clear();
            dgvRotulos.Rows.Clear();
            dgvRotulos.RowHeadersVisible = false;

            dgvRotulos.ReadOnly = true;
            dgvRotulos.AllowUserToAddRows = false;
            dgvRotulos.MultiSelect = false;

            dgvRotulos.Columns.Add("Codigo", "Código");
            dgvRotulos.Columns.Add("Descricao", "Descrição");
            dgvRotulos.Columns.Add("Marca", "Marca");
            dgvRotulos.Columns.Add("Moagem", "Moagem");
            dgvRotulos.Columns.Add("Torra", "Torra");
            dgvRotulos.Columns.Add("Origem", "Origem");

            int selectedIndex = 0;
            foreach (Rotulo obj in ((Main)Application.OpenForms["Main"]).manager.Rotulos.OrderByDescending(x => x.Codigo))
            {
                int aux = dgvRotulos.Rows.Add(obj.Codigo, obj.Descricao, Enums.Format(obj.Marca.ToString()), Enums.Format(obj.Moagem.ToString()), Enums.Format(obj.Torra.ToString()), Enums.Format(obj.Origem.ToString()));
                if (!String.IsNullOrEmpty(code) && obj.Codigo == code)
                {
                    selectedIndex = aux;
                }
            }

            if (dgvRotulos.Rows.Count > 0)
            {
                dgvRotulos.Rows[selectedIndex].Selected = true;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            FormRotulo(Convert.ToString(dgvRotulos.SelectedRows[0].Cells[0].Value), false);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string rotuloId = Convert.ToString(dgvRotulos.SelectedRows[0].Cells[0].Value);
            string rotuloDescricao = Convert.ToString(dgvRotulos.SelectedRows[0].Cells[1].Value);

            if (((Main)Application.OpenForms["Main"]).manager.Avaliacoes.Where(x => x.Rotulo.Codigo == rotuloId).Count() == 0)
            {
                DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja remover o Rótulo \"" + rotuloId + " - " + rotuloDescricao + "\"?", "Confirmação", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ((Main)Application.OpenForms["Main"]).manager.Remove(((Main)Application.OpenForms["Main"]).manager.Rotulos.Where(x => x.Codigo == rotuloId).First());
                    ResetForm();
                }
            }
            else
            {
                MessageBox.Show("Impossível remover o rótulo \"" + rotuloId + " - " + rotuloDescricao + "\": Existem avaliações relacionadas à ele.");
            }
        }

        private void dgvRotulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRotulos.SelectedRows.Count == 1)
            {
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
                FormRotulo(Convert.ToString(dgvRotulos.SelectedRows[0].Cells[0].Value), true);
            }
            else
            {
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            FormRotulo();
        }

        private void FormRotulo(string codigo = "", bool readOnly = false)
        {
            this.codigo = codigo;
            ResetFormRotulo();

            if (!String.IsNullOrEmpty(codigo))
            {
                FillFormRotulo();
            }

            foreach (var control in pnlDados.Controls)
            {
                if (control is Button)
                {
                    ((Button)control).Visible = !readOnly;
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
            }

            pnlDados.Visible = true;
        }

        private void FillFormRotulo()
        {
            Rotulo obj = ((Main)Application.OpenForms["Main"]).manager.Rotulos.Where(x => x.Codigo == codigo).First();

            txtCodigo.Text = obj.Codigo;
            txtDescricao.Text = obj.Descricao;

            cmbMarca.SelectedIndex = cmbMarca.FindString(Enums.Format(obj.Marca.ToString()));
            cmbMoagem.SelectedIndex = cmbMoagem.FindString(Enums.Format(obj.Moagem.ToString()));
            cmbTorra.SelectedIndex = cmbTorra.FindString(Enums.Format(obj.Torra.ToString()));
            cmbOrigem.SelectedIndex = cmbOrigem.FindString(Enums.Format(obj.Origem.ToString()));

            foreach (Enums.Sabor value in obj.Sabores)
            {
                string val = Enums.Format(value.ToString());
                lstSabores.Items.Add(val);
                cmbSabores.Items.Remove(cmbSabores.FindStringExact(val));
            }

            foreach (Enums.AfterTaste value in obj.AfterTaste)
            {
                string val = Enums.Format(value.ToString());
                lstAfterTaste.Items.Add(val);
                cmbAfterTaste.Items.Remove(cmbAfterTaste.FindStringExact(val));
            }

            foreach (Enums.Sabor value in obj.Aroma)
            {
                string val = Enums.Format(value.ToString());
                lstAroma.Items.Add(val);
                cmbAroma.Items.Remove(cmbAroma.FindStringExact(val));
            }

            foreach (Enums.Acidez value in obj.Acidez)
            {
                string val = Enums.Format(value.ToString());
                lstAcidez.Items.Add(val);
                cmbAcidez.Items.Remove(cmbAcidez.FindStringExact(val));
            }

            foreach (Enums.Corpo value in obj.Corpo)
            {
                string val = Enums.Format(value.ToString());
                lstCorpo.Items.Add(val);
                cmbCorpo.Items.Remove(cmbCorpo.FindStringExact(val));
            }

            cbxEspecial.Checked = obj.Especial;
            cbxPremium.Checked = obj.Premiado;
            cbxPremiado.Checked = obj.Premiado;
        }

        private void ResetFormRotulo()
        {
            cmbAcidez.Items.Clear();
            cmbAfterTaste.Items.Clear();
            cmbAroma.Items.Clear();
            cmbCorpo.Items.Clear();
            cmbMarca.Items.Clear();
            cmbMoagem.Items.Clear();
            cmbOrigem.Items.Clear();
            cmbSabores.Items.Clear();
            cmbTorra.Items.Clear();

            txtCodigo.Text = String.Empty;
            txtDescricao.Text = String.Empty;

            foreach (Enums.Marca value in Enum.GetValues(typeof(Enums.Marca)))
            {
                string name = Enums.Format(Enum.GetName(typeof(Enums.Marca), value));
                cmbMarca.Items.Add(new Item(value.ToString(), name));
            }

            foreach (Enums.Sabor value in Enum.GetValues(typeof(Enums.Sabor)))
            {
                string name = Enums.Format(Enum.GetName(typeof(Enums.Sabor), value));
                cmbSabores.Items.Add(new Item(value.ToString(), name));
                cmbAroma.Items.Add(new Item(value.ToString(), name));
            }

            foreach (Enums.AfterTaste value in Enum.GetValues(typeof(Enums.AfterTaste)))
            {
                string name = Enums.Format(Enum.GetName(typeof(Enums.AfterTaste), value));
                cmbAfterTaste.Items.Add(new Item(value.ToString(), name));
            }

            foreach (Enums.Origem value in Enum.GetValues(typeof(Enums.Origem)))
            {
                string name = Enums.Format(Enum.GetName(typeof(Enums.Origem), value));
                cmbOrigem.Items.Add(new Item(value.ToString(), name));
            }

            foreach (Enums.Torra value in Enum.GetValues(typeof(Enums.Torra)))
            {
                string name = Enums.Format(Enum.GetName(typeof(Enums.Torra), value));
                cmbTorra.Items.Add(new Item(value.ToString(), name));
            }

            foreach (Enums.Moagem value in Enum.GetValues(typeof(Enums.Moagem)))
            {
                string name = Enums.Format(Enum.GetName(typeof(Enums.Moagem), value));
                cmbMoagem.Items.Add(new Item(value.ToString(), name));
            }

            foreach (Enums.Acidez value in Enum.GetValues(typeof(Enums.Acidez)))
            {
                string name = Enums.Format(Enum.GetName(typeof(Enums.Acidez), value));
                cmbAcidez.Items.Add(new Item(value.ToString(), name));
            }

            foreach (Enums.Corpo value in Enum.GetValues(typeof(Enums.Corpo)))
            {
                string name = Enums.Format(Enum.GetName(typeof(Enums.Corpo), value));
                cmbCorpo.Items.Add(new Item(value.ToString(), name));
            }

            lstAcidez.Items.Clear();
            lstAfterTaste.Items.Clear();
            lstAroma.Items.Clear();
            lstCorpo.Items.Clear();
            lstSabores.Items.Clear();

            cbxEspecial.Checked = false;
            cbxPremium.Checked = false;
            cbxPremiado.Checked = false;

            cmbAcidez.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAfterTaste.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAroma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCorpo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMoagem.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrigem.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSabores.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTorra.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbAcidez.Sorted = true;
            cmbAfterTaste.Sorted = true;
            cmbAroma.Sorted = true;
            cmbCorpo.Sorted = true;
            cmbMarca.Sorted = true;
            cmbMoagem.Sorted = true;
            cmbOrigem.Sorted = true;
            cmbSabores.Sorted = true;
            cmbTorra.Sorted = true;

            // Default values
            try
            {
                string lastCode = ((Main)Application.OpenForms["Main"]).manager.Rotulos.OrderByDescending(x => x.Codigo).First().Codigo;
                string suffix = lastCode.Substring(lastCode.Length - 1);
                string nextCode = "";
                int n = 0;
                if (int.TryParse(suffix, out n))
                {
                    nextCode = lastCode.Substring(0, lastCode.Length - 1) + (n + 1).ToString();
                }
                txtCodigo.Text = nextCode;
            }
            catch { }
            cmbMarca.SelectedIndex = 0;
            cmbMoagem.SelectedIndex = cmbMoagem.FindString(Enums.Format(Enums.Moagem.Espresso.ToString()));
            cmbOrigem.SelectedIndex = cmbOrigem.FindString(Enums.Format(Enums.Origem.Arábica.ToString()));
            cmbTorra.SelectedIndex = cmbTorra.FindString(Enums.Format(Enums.Torra.Média.ToString()));
        }

        private string Persist()
        {
            try
            {
                Rotulo obj, _obj;
                if (String.IsNullOrEmpty(codigo))
                {
                    obj = null;
                    _obj = new Rotulo();
                }
                else
                {
                    obj = ((Main)Application.OpenForms["Main"]).manager.Rotulos.Where(x => x.Codigo == codigo).First();

                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new MemoryStream();
                    using (stream)
                    {
                        formatter.Serialize(stream, obj);
                        stream.Seek(0, SeekOrigin.Begin);
                        _obj = (Rotulo)formatter.Deserialize(stream);
                    }
                }


                _obj.Sabores = new List<Enums.Sabor>();
                _obj.AfterTaste = new List<Enums.AfterTaste>();
                _obj.Aroma = new List<Enums.Sabor>();
                _obj.Acidez = new List<Enums.Acidez>();
                _obj.Corpo = new List<Enums.Corpo>();

                _obj.Codigo = txtCodigo.Text;
                _obj.Descricao = txtDescricao.Text;
                foreach (var item in lstSabores.Items)
                {
                    _obj.Sabores.Add((Enums.Sabor)Enum.Parse(typeof(Enums.Sabor), Enums.Format(item.ToString(), true)));
                }

                _obj.Marca = (Enums.Marca)Enum.Parse(typeof(Enums.Marca), Enums.Format(cmbMarca.SelectedItem.ToString(), true));
                _obj.Moagem = (Enums.Moagem)Enum.Parse(typeof(Enums.Moagem), Enums.Format(cmbMoagem.SelectedItem.ToString(), true));
                _obj.Torra = (Enums.Torra)Enum.Parse(typeof(Enums.Torra), Enums.Format(cmbTorra.SelectedItem.ToString(), true));
                _obj.Origem = (Enums.Origem)Enum.Parse(typeof(Enums.Origem), Enums.Format(cmbOrigem.SelectedItem.ToString(), true));

                foreach (var item in lstAfterTaste.Items)
                {
                    _obj.AfterTaste.Add((Enums.AfterTaste)Enum.Parse(typeof(Enums.AfterTaste), Enums.Format(item.ToString(), true)));
                }
                foreach (var item in lstAcidez.Items)
                {
                    _obj.Acidez.Add((Enums.Acidez)Enum.Parse(typeof(Enums.Acidez), Enums.Format(item.ToString(), true)));
                }
                foreach (var item in lstCorpo.Items)
                {
                    _obj.Corpo.Add((Enums.Corpo)Enum.Parse(typeof(Enums.Corpo), Enums.Format(item.ToString(), true)));
                }
                foreach (var item in lstAroma.Items)
                {
                    _obj.Aroma.Add((Enums.Sabor)Enum.Parse(typeof(Enums.Sabor), Enums.Format(item.ToString(), true)));
                }
                _obj.Especial = cbxEspecial.Checked;
                _obj.Premium = cbxPremium.Checked;
                _obj.Premiado = cbxPremiado.Checked;

                if (((Main)Application.OpenForms["Main"]).manager.Rotulos.Where(x => x.Codigo != codigo && x.Codigo == _obj.Codigo).Count() == 0)
                {
                    if (obj != null)
                    {
                        ((Main)Application.OpenForms["Main"]).manager.Remove(obj);
                    }
                    ((Main)Application.OpenForms["Main"]).manager.Add(_obj);
                }
                else
                {
                    MessageBox.Show("Já existe um rótulo com este código.");
                    return "";
                }
                return _obj.Codigo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                return "";
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Trim().Length == 0 || txtDescricao.Text.Trim().Length == 0)
            {
                MessageBox.Show("Campos em branco");
            }
            else
            {
                string code = Persist();
                if (!String.IsNullOrEmpty(code))
                {
                    FormRotulo();
                    ResetFormRotulo();
                    ResetForm(code);
                }
            }
        }

        private void cmbSabores_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstSabores.Items.Add(cmbSabores.SelectedItem.ToString());
            cmbSabores.Items.Remove(cmbSabores.SelectedItem);
        }

        private void cmbAfterTaste_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstAfterTaste.Items.Add(cmbAfterTaste.SelectedItem.ToString());
            cmbAfterTaste.Items.Remove(cmbAfterTaste.SelectedItem);
        }

        private void cmbAroma_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstAroma.Items.Add(cmbAroma.SelectedItem.ToString());
            cmbAroma.Items.Remove(cmbAroma.SelectedItem);
        }

        private void cmbAcidez_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstAcidez.Items.Add(cmbAcidez.SelectedItem.ToString());
            cmbAcidez.Items.Remove(cmbAcidez.SelectedItem);
        }

        private void cmbCorpo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstCorpo.Items.Add(cmbCorpo.SelectedItem.ToString());
            cmbCorpo.Items.Remove(cmbCorpo.SelectedItem);
        }

        private void lstSabores_DoubleClick(object sender, EventArgs e)
        {
            if (lstSabores.SelectedIndex > -1)
            {
                string item = lstSabores.SelectedItem.ToString();
                lstSabores.Items.Remove(item);
                string itemReverse = Enums.Format(item, true);
                cmbSabores.Items.Add(new Item((Enum.Parse(typeof(Enums.Sabor), itemReverse)).ToString(), item));
            }
        }

        private void lstAfterTaste_DoubleClick(object sender, EventArgs e)
        {
            if (lstAfterTaste.SelectedIndex > -1)
            {
                string item = lstAfterTaste.SelectedItem.ToString();
                lstAfterTaste.Items.Remove(item);
                string itemReverse = Enums.Format(item, true);
                cmbAfterTaste.Items.Add(new Item((Enum.Parse(typeof(Enums.AfterTaste), itemReverse)).ToString(), item));
            }
        }

        private void lstAroma_DoubleClick(object sender, EventArgs e)
        {
            if (lstAroma.SelectedIndex > -1)
            {
                string item = lstAroma.SelectedItem.ToString();
                lstAroma.Items.Remove(item);
                string itemReverse = Enums.Format(item, true);
                cmbAroma.Items.Add(new Item((Enum.Parse(typeof(Enums.Sabor), itemReverse)).ToString(), item));
            }
        }

        private void lstAcidez_DoubleClick(object sender, EventArgs e)
        {
            if (lstAcidez.SelectedIndex > -1)
            {
                string item = lstAcidez.SelectedItem.ToString();
                lstAcidez.Items.Remove(item);
                string itemReverse = Enums.Format(item, true);
                cmbAcidez.Items.Add(new Item((Enum.Parse(typeof(Enums.Acidez), itemReverse)).ToString(), item));
            }
        }

        private void lstCorpo_DoubleClick(object sender, EventArgs e)
        {
            if (lstCorpo.SelectedIndex > -1)
            {
                string item = lstCorpo.SelectedItem.ToString();
                lstCorpo.Items.Remove(item);
                string itemReverse = Enums.Format(item, true);
                cmbCorpo.Items.Add(new Item((Enum.Parse(typeof(Enums.Corpo), itemReverse)).ToString(), item));
            }
        }
    }
}
