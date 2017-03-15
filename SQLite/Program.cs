using System;
using System.Windows.Forms;
using SQLite.View;
using SQLite.Database;

namespace SQLite
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Sqlite database = new Sqlite();           
            database.sqliteDir();
            database.sqliteDatabase();
            database.sqliteTable();
            //database.sqliteConnect();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Primary());
            
        }
    }
}
