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
    public partial class AddDetail : helpers.BaseForm, helpers.IUpdateContract
    {
        public AddDetail()
        {
            InitializeComponent();
        }

        private void AddDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Берем из бэкстека последнюю форму
            // и перед открытием обновляем данные
            Details forma = (Details)helpers.Helper.BackStack;
            forma.onUpdateUI();
            forma.Show();
        }

        private void AddDetail_Load(object sender, EventArgs e)
        {
            onUpdateUI();
        }

        private void configureData()
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "autoserviceDataSet.spare". При необходимости она может быть перемещена или удалена.
            this.spareTableAdapter.Fill(this.autoserviceDataSet.spare);
        }

        private void addCarButton_Click(object sender, EventArgs e)
        {
            if (markTextBox.Text != "" && modelTextBox.Text != "")
            {
                var confirmResult = MessageBox.Show("Вы действительно хотите добавить деталь?",
                                     "Добавление",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    spareTableAdapter.Insert(
                        markTextBox.Text,
                        modelTextBox.Text,
                        (int)countNumericUpDown.Value
                    );
                    MessageBox.Show("Деталь добавлена");
                    Close();
                }
            } else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }

        public void onUpdateUI()
        {
            configureData();
        }
    }
}
