using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznicek.Classes
{
    public class DBhelper
    {
        private SQLiteConnection db = new SQLiteConnection("DATABASE.db");

        public DBhelper()
        {
            db.CreateTable<Outlay>();
            db.CreateTable<Category>();
            db.CreateTable<Debt>();
        }
        public void Insert<T>(T table) where T : ATable, new()
        {
            db.Insert(table);
        }
        public void InsertWithChildren<T>(T table) where T : ATable, new()
        {
            db.InsertWithChildren(table, true);
        }
        public List<T> GetAllWithChildren<T>() where T : ATable, new()
        {
            return db.GetAllWithChildren<T>().ToList();
        }
        public T GetWithChildrenById<T>(int id) where T : ATable, new()
        {
            return db.GetWithChildren<T>(id);
        }

        public void UpdateWithChildren<T>(T table) where T : class, new()
        {
            db.UpdateWithChildren(table);
        }
        public void Delete<T>(T table) where T : ATable, new()
        {
            db.Table<T>().Delete(v => v.Id.Equals(table.Id));
        }
        public void DeleteCategory(Category category)
        {
            db.Table<Outlay>().Delete(v => v.CategoryId.Equals(category.Id));
            Delete(category);
        }
        public void DeleteDebt(Debt debt)
        {
            db.Table<Outlay>().Delete(v => v.DebtId.Equals(debt.Id));
            Delete(debt);
        }
        /*
        public void UpdateOutlay(Outlay outlay, DateTime date, string name, int price)
        {
            outlay.Date = date;
            outlay.Name = name;
            outlay.Price = price;

            var x = db.Update(outlay);
            RefreshData();
        }
        */
    }
}
