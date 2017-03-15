using System;
using System.IO;
using System.Data.SQLite;
using System.Windows.Forms;
using SQLite.Entidade;

namespace SQLite.Database
{
    class Sqlite : InterfaceSqlite
    {
        private string DB_DIR = "Database";
        private static string DB_NAME = "pessoa.db";
        private string DB_URL = @"Data Source=Database\"+ DB_NAME + ";Version=3;datetimeformat=CurrentCulture";
        
        // Cria um diretório onde dentro dele será armazenado o database, sqliteDatabase
        public void sqliteDir()
        {
            if (!Directory.Exists(DB_DIR))
            {
                Directory.CreateDirectory(DB_DIR);
            }          
        }
        
        // Cria um database;
        public void sqliteDatabase()
        {
            bool fileExist = File.Exists(DB_DIR + @"\" + DB_NAME);

            if (!fileExist){
                SQLiteConnection.CreateFile(DB_DIR + @"\" + DB_NAME);
            }           
        }

        // Testa conexão com o database empresa.db
        public void sqliteConnect()
        {
            using (SQLiteConnection con = new SQLiteConnection(DB_URL))
            {
                try
                {
                    con.Open();

                    MessageBox.Show("Conectado");

                    con.Close();                  
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // Cria tabelas
        public void sqliteTable()
        {
            using(SQLiteConnection con = new SQLiteConnection(DB_URL))
            {
                try
                {
                    con.Open();

                    string sql =
                    "CREATE TABLE IF NOT EXISTS PESSOA"
                  + "("
                  + "CODIGO INTEGER PRIMARY KEY AUTOINCREMENT,"
                  + "NOME TEXT NOT NULL,"
                  + "SOBRENOME TEXT NOT NULL,"
                  + "DATA_NASCIMENTO DATE NOT NULL,"
                  + "SEXO TEXT NOT NULL"
                  + ");";

                    using (SQLiteCommand command = new SQLiteCommand(sql, con))
                        command.ExecuteNonQuery();

                    con.Close();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
            }         
        }

        // Insere dados, nome, idade
        public void sqliteInsert(Pessoa pessoa)
        {
            using (SQLiteConnection con = new SQLiteConnection(DB_URL))
            {
                try
                {
                    con.Open();

                    string sql = "INSERT INTO PESSOA (NOME, SOBRENOME, DATA_NASCIMENTO, SEXO) VALUES ('" + pessoa.Nome + "','" + pessoa.Sobrenome + "','" + pessoa.DataNascimento + "','" + pessoa.Sexo + "');";

                    using (SQLiteCommand command = new SQLiteCommand(sql, con))
                        command.ExecuteNonQuery();

                    con.Close();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
      

        }

        // Verifica e mostra os resultados do statment
        public void sqliteSelect(RichTextBox rich)
        {
            using(SQLiteConnection con = new SQLiteConnection(DB_URL))
            {
                try
                {
                    con.Open();

                    string sql = "SELECT * FROM PESSOA;";

                    using (SQLiteCommand command = new SQLiteCommand(sql, con))
                    {
                        using (SQLiteDataReader dataReader = command.ExecuteReader())
                        {
                            rich.Text = "";
                            while (dataReader.Read())
                            {
                                rich.Text += "Código: " + dataReader["CODIGO"] + "\n";
                                rich.Text += "Nome: " + dataReader["NOME"] + " ";
                                rich.Text += dataReader["SOBRENOME"] + "\n";
                                rich.Text += "Nascimento: " + dataReader["DATA_NASCIMENTO"].ToString().Substring(0, 10) + "\n";
                                rich.Text += "Sexo: " + dataReader["SEXO"] + "\n \n";

                            }
                            dataReader.Close();
                        }
                        
                    }
                    
                    con.Close();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            
            
        }

        public void sqliteDelete(int codigo)
        {
            using (SQLiteConnection con = new SQLiteConnection(DB_URL))
            {
                try
                {
                    con.Open();

                    string sql = "DELETE FROM PESSOA WHERE CODIGO = " + codigo + ";";

                    using (SQLiteCommand command = new SQLiteCommand(sql, con))
                        command.ExecuteNonQuery();

                    con.Close();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
            }          
        }

        public bool sqliteVerificaDeletar(int valorBox)
        {
            bool validaCampo = false;
            using (SQLiteConnection con = new SQLiteConnection(DB_URL))
            {
                try
                {
                    con.Open();

                    string sql = "SELECT CODIGO FROM PESSOA;";

                    using (SQLiteCommand command = new SQLiteCommand(sql, con))
                    {
                        using (SQLiteDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                int codigo = Convert.ToInt32(dataReader["CODIGO"]);

                                if (valorBox == codigo)
                                {
                                    validaCampo = true;
                                    break;
                                }
                            }
                            dataReader.Close();
                        }                        
                    }
                        
                    con.Close();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            return validaCampo;
        }

    }
}
