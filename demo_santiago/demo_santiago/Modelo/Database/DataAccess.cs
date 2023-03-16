using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace demo_santiago
{
    public class DataAccess
    {
        SQLiteAsyncConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IUtilities>();
            connection = new SQLiteAsyncConnection(Path.Combine(config.DirectoryBD, "Messages.db3"));
            connection.CreateTableAsync<Messages_DB>();
        }

        public void InserMessage(Messages_DB message)
        {
            connection.InsertAsync(message);
        }

        public void DeleteMessage(Messages_DB message)
        {
            connection.DeleteAsync(message);
        }

        public async Task<List<Messages_DB>> GetAllMessages()
        {
            return await connection.Table<Messages_DB>().OrderByDescending(e => e.Date_Created).ToListAsync();
        }

        public async Task<List<Messages_DB>> GetGroupByMessages(string initDate = null, string endDate = null)
        {
            string lastmonth = "";
            string today = "";
            if (initDate == null || endDate == null)
            {
                lastmonth = DateTime.Today.AddMonths(-3).ToString("yyyy-MM-dd");
                today = DateTime.Today.ToString("yyyy-MM-dd");
            }
            else
            {
                lastmonth = DateTime.Parse(initDate).ToString("yyyy-MM-dd");
                today = DateTime.Parse(endDate).ToString("yyyy-MM-dd");
            }
            

            List<Messages_DB> allRows = await connection.Table<Messages_DB>().ToListAsync();
            List<List<Messages_DB>> groupedCustomerList = allRows
                                    .Where(element => (DateTime.Parse(element.Date_Created.Split(' ')[0]) >= DateTime.Parse(lastmonth) && DateTime.Parse(element.Date_Created.Split(' ')[0]) <= DateTime.Parse(today)))
                                    .GroupBy(u => u.Message.ToUpper())
                                    .Select(grp => grp.ToList())
                                    .ToList();

            List<Messages_DB> cleanList = new List<Messages_DB>();

            foreach (var item in groupedCustomerList)
            {
                cleanList.Add(item[0]);
            }

            return cleanList;
        }
    }
}

