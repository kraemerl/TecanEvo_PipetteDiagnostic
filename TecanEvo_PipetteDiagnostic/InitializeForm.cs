using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TecanEvo_PipetteDiagnostic
{
    public partial class InitializeForm : Form
    {
        /// <summary>
        /// retVal: 0 - all OK
        /// retVal: 1 - all OK, load variables
        /// retVal: 2 - Errors, abort Evoware script
        /// </summary>
        private int m_retVal = 2;
        private int m_micronic_rack_used = 1;

        private string[] m_args;

        public InitializeForm(string[] args)
        {
            m_args = args;
            InitializeComponent();
        }

        private void InitializeForm_Load(object sender, EventArgs e)
        {
            cbEppiRacksToScan.SelectedIndex = 1;
        }

        public int returnValue()
        {
            return m_retVal;
        }

        private void chkMicronicRack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMicronicRack.Checked == false)
            {
                m_micronic_rack_used = 0;
                txtMicronicRackBarcode.Enabled = false;
                txtMicronicRackBarcode.Text = "";
            }
            else
            {
                m_micronic_rack_used = 1;
                txtMicronicRackBarcode.Enabled = true;
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            m_retVal = 2;
            Application.Exit();
        }

        private void writeVariables(string file)
        {
            try
            {
                var fileStream = new FileStream(@file, FileMode.Create, FileAccess.ReadWrite);
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write("I,eppi_racks_to_scan," + cbEppiRacksToScan.Text + "\r\n");
                    streamWriter.Write("I,micronic_rack_used," + m_micronic_rack_used + "\r\n");
                    streamWriter.Write("S,micronic_rack_barcode," + txtMicronicRackBarcode.Text.Trim() + "\r\n");
                    streamWriter.Write("S,plate_96well," + txt96wellPlateBarcode.Text.Trim() + "\r\n");
                    streamWriter.Write("I,target_96well_volume," + int.Parse(txt96wellTargetVolume.Text.Trim()) + "\r\n");
                    streamWriter.Write("I,target_96well_concentration," + int.Parse(txt96wellTargetConcentration.Text.Trim()) + "\r\n");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
                throw new Exception("Error writing variables file: \"" + file + "\"\r\n" + e.Message);
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (chkMicronicRack.Checked)
            {
                if (txtMicronicRackBarcode.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a Micronic rack barcode.");
                    txtMicronicRackBarcode.Focus();
                    return;
                }
                else
                {
                    if (CSQL.checkMicronicRackExist(txtMicronicRackBarcode.Text.Trim()) == false)
                    {
                        MessageBox.Show("Rack " + txtMicronicRackBarcode.Text.Trim() + " does not exist.");
                        txtMicronicRackBarcode.Focus();
                        return;
                    }
                }
            }

            if (txt96wellPlateBarcode.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a 96well plate barcode.");
                txt96wellPlateBarcode.Focus();
                return;
            }
            else
            {
                if (CSQL.checkPlate96wellExist(txt96wellPlateBarcode.Text.Trim()) == false)
                {
                    MessageBox.Show("Plate " + txt96wellPlateBarcode.Text.Trim() + " does not exist.");
                    txt96wellPlateBarcode.Focus();
                    return;
                }
            }

            writeVariables(m_args[1]);
            m_retVal = 1;
            Application.Exit();
        }

        private void txt96wellTargetVolume_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)'0':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'1':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'2':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'3':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'4':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'5':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'6':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'7':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'8':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'9':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)Keys.Back:
                    e.KeyChar = e.KeyChar;
                    break;
                default:
                    e.KeyChar = (char)Keys.None;
                    break;
            }
        }

        private void txt96wellTargetConcentration_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)'0':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'1':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'2':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'3':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'4':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'5':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'6':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'7':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'8':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)'9':
                    e.KeyChar = e.KeyChar;
                    break;
                case (char)Keys.Back:
                    e.KeyChar = e.KeyChar;
                    break;
                default:
                    e.KeyChar = (char)Keys.None;
                    break;
            }
        }

        private void txt96wellTargetVolume_TextChanged(object sender, EventArgs e)
        {
            if (txt96wellTargetVolume.Text.Trim() == "")
            {
                txt96wellTargetVolume.Text = "5";
            }
        }

        private void txt96wellTargetConcentration_TextChanged(object sender, EventArgs e)
        {
            if (txt96wellTargetVolume.Text.Trim() == "")
            {
                txt96wellTargetVolume.Text = "1";
            }
        }
    }
}
