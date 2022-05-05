namespace Calculadora
{

    public partial class Form1 : Form
    {       
        //Variaveis globais
        Double resultValue = 0;
        String operationPerformed = "";
        bool isOperationPerformed = false;


        public Form1()
        {
            InitializeComponent();
        }

        

        private void buttonOne_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonTwo_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonThree_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonFour_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonFive_Click(object sender, EventArgs e)
        {
            

        }

        private void buttonSix_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonSeven_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = textBoxResult.Text + "7";
        }

        private void buttonEight_Click(object sender, EventArgs e)
        {

            
        }

        private void buttonNine_Click(object sender, EventArgs e)
        {
            
            
        }

        private void buttonZero_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonPoint_Click(object sender, EventArgs e)
        {
            
            
        }

        private void operator_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (resultValue != 0)
            {
                buttonEqual.PerformClick();
                operationPerformed = button.Text;
                labelCurrentOperation.Text = resultValue + " " + operationPerformed;
                isOperationPerformed = true;
            }
            else
            {
                operationPerformed = button.Text;
                resultValue = Double.Parse(textBoxResult.Text);
                labelCurrentOperation.Text = resultValue + " " + operationPerformed;
                isOperationPerformed = true;
            }
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = "0";           
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = "0";
            resultValue = 0;
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            try
            {
                switch (operationPerformed)
                {
                    case "+":
                        textBoxResult.Text = (resultValue + Double.Parse(textBoxResult.Text)).ToString();
                        break;

                    case "-":
                        textBoxResult.Text = (resultValue - Double.Parse(textBoxResult.Text)).ToString();
                        break;

                    case "X":
                        textBoxResult.Text = (resultValue * Double.Parse(textBoxResult.Text)).ToString();
                        break;

                    case "/":
                        textBoxResult.Text = (resultValue / Double.Parse(textBoxResult.Text)).ToString();
                        break;
                    default:
                        break;
                }
                resultValue = Double.Parse(textBoxResult.Text);
                labelCurrentOperation.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro, tente novamente");
            }
            
        }

        private void button_click(object sender, MouseEventArgs e)
        {
            if ((textBoxResult.Text == "0") || (isOperationPerformed))
                textBoxResult.Clear();

            isOperationPerformed = false;
            Button button = (Button)sender;
            if (button.Text == ".")
            {
                if (!textBoxResult.Text.Contains('.'))
                    textBoxResult.Text = textBoxResult.Text + button.Text;

            }
            else
                textBoxResult.Text = textBoxResult.Text + button.Text;
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {

        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {

        }

        
    }
}