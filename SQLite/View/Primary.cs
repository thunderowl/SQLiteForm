using System;
using System.Windows.Forms;
using SQLite.Entidade;
using SQLite.Database;

namespace SQLite.View
{
    public partial class Primary : Form
    {
        Sqlite sqlite = new Sqlite();
        int codigo;

        public Primary()
        {
            InitializeComponent();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void buttonMenuCadastrar_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Aguardando ação do usuário...";
            panelDeletarPessoa.Visible = false;
            panelBemVindo.Visible = false;
            panelListaPessoas.Visible = false;
            panelAtualiza.Visible = false;
            panelConsulta.Visible = false;
            panelMostraConsulta.Visible = false;
            panelVerdadeAtualiza.Visible = false;
            panelCadastrar.Visible = true;
        }

        private void buttonLimpaCadastro_Click(object sender, EventArgs e)
        {
            textBoxNomeCadastro.Clear();
            textBoxSobrenomeCadastro.Clear();
            radioButtonSexoM.Select();
            dateTimePickerCadastro.Value = new DateTime (2017, 03, 11);
        }

        private void buttonMenuListar_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Listando pessoas...";
            panelBemVindo.Visible = false;
            panelDeletarPessoa.Visible = false;
            panelCadastrar.Visible = false;
            panelAtualiza.Visible = false;
            panelConsulta.Visible = false;
            panelMostraConsulta.Visible = false;
            panelVerdadeAtualiza.Visible = false;
            panelListaPessoas.Visible = true;
            sqlite.sqliteSelect(richTextBoxListaPessoa);
            toolStripStatusLabel1.Text = "Listagem concluída...";
        }

        private void buttonEnviaCadastro_Click(object sender, EventArgs e)
        {           
            string nome = textBoxNomeCadastro.Text;
            string sobrenome = textBoxSobrenomeCadastro.Text;           
            string data = dateTimePickerCadastro.Value.Date.ToShortDateString();
            string sexo = radioChekedCadastro();

            if (!nome.Equals(null) && !sobrenome.Equals(null) && !nome.Equals("") && !sobrenome.Equals(""))
            {
                Pessoa pessoa = new Pessoa(nome, sobrenome, data, sexo);
                sqlite.sqliteInsert(pessoa);
                toolStripStatusLabel1.Text = "Inserido com sucesso.";
                buttonLimpaCadastro.PerformClick();
            }
            else
            {
                toolStripStatusLabel1.Text = "Verifique os campos, nenhum pode ter valor nulo.";
            }           
        }

        private string radioChekedCadastro()
        {
            string ch = null;

            if (radioButtonSexoM.Checked)
            {
                ch = radioButtonSexoM.Text;
            }
            else if (radioButtonSexoF.Checked)
            {
                ch = radioButtonSexoF.Text;
            }
            else if (radioButtonSexoN.Checked)
            {
                ch = radioButtonSexoN.Text;
            }

            return ch;
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form sobre = new Sobre();
            sobre.ShowDialog();
        }

        private void buttonMenuDeletar_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Aguardando ação do usuário...";
            panelCadastrar.Visible = false;
            panelBemVindo.Visible = false;
            panelListaPessoas.Visible = false;
            panelAtualiza.Visible = false;
            panelConsulta.Visible = false;
            panelMostraConsulta.Visible = false;
            panelVerdadeAtualiza.Visible = false;
            panelDeletarPessoa.Visible = true;
        }

        private void buttonEnviaDeletar_Click(object sender, EventArgs e)
        {
            if (!textBoxDeletarCodigo.Text.Equals("") && !textBoxDeletarCodigo.Text.Equals(null))
            {
                int codigo = int.Parse(textBoxDeletarCodigo.Text);

                if (sqlite.sqliteVerificaCod(codigo))
                {
                    sqlite.sqliteDelete(codigo);
                    textBoxDeletarCodigo.Text = null;
                    toolStripStatusLabel1.Text = "Deletado com sucesso.";
                }
                else
                {
                    toolStripStatusLabel1.Text = "Código inválido.";
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Verifique os campos, nenhum pode ter valor nulo.";
            }
            
        }

        private void buttonMenuAtualizar_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Aguardando ação do usuário...";
            panelBemVindo.Visible = false;
            panelCadastrar.Visible = false;
            panelDeletarPessoa.Visible = false;
            panelListaPessoas.Visible = false;
            panelConsulta.Visible = false;
            panelMostraConsulta.Visible = false;
            panelVerdadeAtualiza.Visible = false;
            panelAtualiza.Visible = true;
        }

        private void buttonMenuConsultar_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Aguardando ação do usuário...";
            panelBemVindo.Visible = false;
            panelCadastrar.Visible = false;
            panelListaPessoas.Visible = false;
            panelDeletarPessoa.Visible = false;
            panelAtualiza.Visible = false;
            panelMostraConsulta.Visible = false;
            panelVerdadeAtualiza.Visible = true;
            panelConsulta.Visible = true;
        }

        private void buttonEnviarConsulta_Click(object sender, EventArgs e)
        {
            if (!textBoxCodConsulta.Text.Equals("") && !textBoxCodConsulta.Text.Equals(null))
            {
                int codigo = int.Parse(textBoxCodConsulta.Text);

                if (sqlite.sqliteVerificaCod(codigo))
                {
                    panelMostraConsulta.Visible = true;
                    sqlite.sqliteFind(codigo, richTextBoxMostraConsulta);
                    textBoxCodConsulta.Text = null;
                    toolStripStatusLabel1.Text = "Consulta feita com sucesso.";
                }
                else
                {
                    toolStripStatusLabel1.Text = "Código inválido.";
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Verifique os campos, nenhum pode ter valor nulo.";
            }
            
        }
        
        private void buttonEnviaAtualizar_Click(object sender, EventArgs e)
        {
            codigo = int.Parse(textBoxCodAtualiza.Text);

            if (!textBoxCodAtualiza.Text.Equals("") && !textBoxCodAtualiza.Text.Equals(null))
            {
                int codigo = int.Parse(textBoxCodAtualiza.Text);

                if (sqlite.sqliteVerificaCod(codigo))
                {
                    toolStripStatusLabel1.Text = "Informe os campos para atualizar.";
                    panelVerdadeAtualiza.Visible = true;
                    string sexo = null;

                    sqlite.sqliteMostraAtualiza(codigo, textBoxAtualizaNome, textBoxAtualizaSobrenome, dateTimePickerAtualiza, ref sexo);

                    if (sexo[0].Equals('M'))
                        radioButtonAtualizaSexoM.Checked = true;
                    else if (sexo[0].Equals('F'))
                        radioButtonAtualizaSexoF.Checked = true;
                    else if (sexo[0].Equals('N'))
                        radioButtonAtualizaSexoN.Checked = true;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Código inválido.";
                }
            }
            
        }

        private string radioChekedAtualiza()
        {
            string ch = null;

            if (radioButtonAtualizaSexoM.Checked)
            {
                ch = radioButtonAtualizaSexoM.Text;
            }
            else if (radioButtonAtualizaSexoF.Checked)
            {
                ch = radioButtonAtualizaSexoF.Text;
            }
            else if (radioButtonAtualizaSexoN.Checked)
            {
                ch = radioButtonAtualizaSexoN.Text;
            }

            return ch;
        }
        
        private void buttonAtualizaCancela_Click(object sender, EventArgs e)
        {
            textBoxCodAtualiza.Text = null;
            panelVerdadeAtualiza.Visible = false;
            toolStripStatusLabel1.Text = "Aguardando ação do usuário...";
        }

        private void buttonEnviaAtualiza2_Click(object sender, EventArgs e)
        {
            textBoxCodAtualiza.Text = null;

            Pessoa pessoa = new Pessoa(textBoxAtualizaNome.Text, textBoxAtualizaSobrenome.Text, dateTimePickerAtualiza.Value.Date.ToShortDateString(), radioChekedAtualiza());
            sqlite.sqliteUpdate(codigo, pessoa);

            toolStripStatusLabel1.Text = "Atualizado com sucesso...";
        }

        //cadastra
        #region cadastra
        private void textBoxNomeCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaCadastro.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxSobrenomeCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaCadastro.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void radioButtonSexoF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaCadastro.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void radioButtonSexoM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaCadastro.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void radioButtonSexoN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaCadastro.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void dateTimePickerCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaCadastro.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        #endregion
        //deleta
        #region deleta
        private void textBoxDeletarCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaDeletar.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        #endregion
        //consulta
        #region consulta
        private void textBoxCodConsulta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviarConsulta.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        #endregion
        //atualiza
        #region atualiza

        private void textBoxCodAtualiza_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaAtualizar.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxAtualizaNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaAtualiza2.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxAtualizaSobrenome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaAtualiza2.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        
        private void radioButtonAtualizaSexoM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaAtualiza2.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void radioButtonAtualizaSexoN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaAtualiza2.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void radioButtonAtualizaSexoF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaAtualiza2.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        

        private void dateTimePickerAtualiza_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviaAtualiza2.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        #endregion

        //fim atualiza
    }
}
