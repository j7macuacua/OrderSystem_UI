using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//---------------------------
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace OrderSystem_UI.app
{
    class ConfigDataConnection
    {
        string folder="Config";
        public void createFolder()
        {
            string folder = "Config";
        }
        //===============SQL data
        String _dbConnectionSQL = "SqlConnection";
        String _dbCommandSQL = "SqlCommand";
        String _dbDataReaderSQL = "SqlDataReader";

        String _dbDataAdapterSQL = "SqlDataAdapter";
        String _dbCommandBuilderSQL = "SqlCommandBuilder";

        StreamWriter sw, swr;

        
        public void sqlDataConnection()
        {
            string ficheiro = @"DBSystem.txt";
            if (File.Exists(folder + "\\" + ficheiro) == false)
            {

                sw = File.CreateText(folder + "\\" + ficheiro);
            }
            string linha = _dbConnectionSQL + ";" + _dbCommandSQL + ";" + _dbDataReaderSQL + ";" + _dbDataAdapterSQL + ";" + _dbCommandBuilderSQL;
            sw.WriteLine(linha);

            MessageBox.Show("Connection File Successfully Created ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sw.Close();
        }
        //===============endSQL data

        //===============MySQL data
        String _dbConnectionMySQL = "MySqlConnection";
        String _dbCommandMySQL = "MySqlCommand";
        String _dbDataReaderMySQL = "MySqlDataReader";

        String _dbDataAdapterMySQL = "MySqlDataAdapter";
        String _dbCommandBuilderMySQL = "MySqlCommandBuilder";

        public void mySqlDataConnection()
        {
            
            string ficheiro = @"DBSystem.txt";
            if (File.Exists(folder + "\\" + ficheiro) == false)
            {
                swr = File.CreateText(folder + "\\" + ficheiro);
            }
            string linha = _dbConnectionMySQL + ";" + _dbCommandMySQL + ";" + _dbDataReaderMySQL + ";" + _dbDataAdapterMySQL + ";" + _dbCommandBuilderMySQL;
            swr.WriteLine(linha);

            MessageBox.Show("Connection File Successfully Created ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            swr.Close();
            Application.Restart();
        }
        //===============endMySQL data

        
    }
}
