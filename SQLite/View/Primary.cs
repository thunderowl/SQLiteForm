using System;
using System.Windows.Forms;
using SQLite.Entidade;
using SQLite.Database;

namespace SQLite.View
{
    public partial class Primary : Form
    {
        Sqlite sqlite = new Sqlite();

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

            if (nome != String.Empty || sobrenome != String.Empty)
            {
                Pessoa pessoa = new Pessoa(nome, sobrenome, data, sexo);
                sqlite.sqliteInsert(pessoa);
                toolStripStatusLabel1.Text = "Inserido com sucesso.";
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
            if (!textBoxDeletarCodigo.Text.Equals(""))
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
            if (!textBoxCodConsulta.Equals(""))
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
            if(!textBoxCodAtualiza.Equals(""))
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
        }

        private void buttonEnviaAtualiza2_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(textBoxCodAtualiza.Text);
            textBoxCodAtualiza.Text = null;

            Pessoa pessoa = new Pessoa(textBoxAtualizaNome.Text, textBoxAtualizaSobrenome.Text, dateTimePickerAtualiza.Value.Date.ToShortDateString(), radioChekedAtualiza());
            sqlite.sqliteUpdate(codigo, pessoa);

            toolStripStatusLabel1.Text = "Atualizado com sucesso...";
        }
    }
}
