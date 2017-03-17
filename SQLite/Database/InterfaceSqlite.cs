using SQLite.Entidade;
using System.Windows.Forms;

namespace SQLite.Database
{
    interface InterfaceSqlite
    {
        void sqliteDir();
        void sqliteDatabase();
        void sqliteConnect();
        void sqliteTable();
        void sqliteInsert(Pessoa pessoa);
        void sqliteSelect(RichTextBox rich);
        void sqliteDelete(int codigo);
        bool sqliteVerificaCod(int valorBox);
    }
}
