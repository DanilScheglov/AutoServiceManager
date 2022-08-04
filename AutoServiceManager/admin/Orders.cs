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
    public partial class Orders : helpers.BaseForm, helpers.IUpdateContract
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            onUpdateUI();
        }

        public void onUpdateUI()
        {
            configureData();
            var orders = (from order in autoserviceDataSet.order
                          orderby order.id
                          join car in autoserviceDataSet.car on order.car_id equals car.id
                          join client in autoserviceDataSet.client on car.client_id equals client.id
                          select new
                          {
                              order.id,
                              Машина = car.mark + " " + car.model,
                              Клиент = client.first_name + " " + client.middle_name + " " + client.last_name,
                              Ремонт = order.repair_type,
                              Начало = order.start_date,
                              Статус = order.status
                          }).ToList();

            ordersDataGridView.DataSource = orders;
            ordersDataGridView.AutoResizeColumns();
            ordersDataGridView.ClearSelection();
            ordersDataGridView.Columns[0].Visible = false;
            ordersDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11);
        }

        private void configureData()
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "autoserviceDataSet.order". При необходимости она может быть перемещена или удалена.
            this.orderTableAdapter.Fill(this.autoserviceDataSet.order);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "autoserviceDataSet.car". При необходимости она может быть перемещена или удалена.
            this.carTableAdapter.Fill(this.autoserviceDataSet.car);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "autoserviceDataSet.client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.autoserviceDataSet.client);
        }

        private void addOrderButton_Click(object sender, EventArgs e)
        {
            helpers.Helper.BackStack = this;
            new AddOrder().Show();
        }

        private void Orders_FormClosing(object sender, FormClosingEventArgs e)
        {
            helpers.Helper.Quit(false);
        }
    }
}
