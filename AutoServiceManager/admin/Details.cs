using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoServiceManager.admin
{
    public partial class Details : helpers.BaseForm, helpers.IUpdateContract
    {
        public Details()
        {
            InitializeComponent();
        }

        private void Details_Load(object sender, EventArgs e)
        {
            onUpdateUI();
        }

        private void configureData()
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "autoserviceDataSet.spare". При необходимости она может быть перемещена или удалена.
            this.spareTableAdapter.Fill(this.autoserviceDataSet.spare);
        }

        public void onUpdateUI()
        {
            configureData();
            var details = (from s in autoserviceDataSet.spare
                          orderby s.id
                          select new {
                              Деталь = s.mark + " " + s.model,
                              Количество = s.count
                          }).ToList();

            detailsDataGridView.DataSource = details;
            detailsDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11);
            detailsDataGridView.ColumnHeadersHeight = 40;
            detailsDataGridView.AutoResizeColumns();
            detailsDataGridView.ClearSelection();
        }

        private void addDetailButton_Click(object sender, EventArgs e)
        {
            // Закладываем в бэкстек текущую форму, которую надо
            // скрыть и открыть следующую
            helpers.Helper.BackStack = this;
            new AddDetail().Show();
        }

        private void Details_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Показать самую последнюю скрытую, ну и закрыть текущую
            helpers.Helper.Quit(false);
        }
    }
}
